namespace WebSharper.Ollama.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.Ollama

[<JavaScript>]
module Client =
    // The templates are loaded from the DOM, so you just can edit index.html
    // and refresh your browser, no need to recompile unless you add or remove holes.
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

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
            .GenerateTest(fun _ -> 
                async {
                    return! GenerateTest().Then(fun response -> printfn $"Response: {response.Response}").AsAsync()
                }
                |> Async.Start
            )
            .Doc()
        |> Doc.RunById "main"
