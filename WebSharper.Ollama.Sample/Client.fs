namespace WebSharper.Ollama.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.Ollama

[<JavaScript>]
module Client =
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>
    let ChatResponse = Var.Create ""
    
    let People =
        ListModel.FromSeq [
            "John"
            "Paul"
        ]

    let Ollama = new Ollama(Config(host = "http://localhost:5555"))

    let GenerateTest() = promise {
        let request = GenerateRequest(
            model = "llama3.1",
            prompt = "Why is the sky blue?"
        )   
        let! response = Ollama.Generate(request)

        return response;
    }
    
    let ChatTest() = promise {
        let request = ChatRequest(
            model = "llama3.1",
            Messages = [|Message(role = "user", content = "Why is the sky blue?")|]
        )   
        let! response = Ollama.Chat(request)

        return response;
    }

    [<SPAEntryPoint>]
    let Main () =
        let newName = Var.Create ""


        IndexTemplate.Main()
            .ListContainer(
                People.View.DocSeqCached(fun (name: string) ->
                    IndexTemplate.ListItem().Name(name).Doc()
                )
            )
            .Name(newName)
            .Add(fun _ ->
                People.Add(newName.Value)
                newName.Value <- ""
            )
            .Generate(fun _ -> 
                async {
                    return! GenerateTest().Then(fun response -> printfn $"Response: {response.Response}").AsAsync()
                }
                |> Async.Start
            )
            .Chat(fun _ -> 
                async {
                    return! ChatTest().Then(fun response -> 
                    Var.Set ChatResponse <| response.Message.Content
                    printfn $"Response: {response.Message.Content}").AsAsync()
                }
                |> Async.Start
            )
            .ChatResponse(ChatResponse.V)
            .Doc()
        |> Doc.RunById "main"
