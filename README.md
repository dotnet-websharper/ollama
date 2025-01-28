# WebSharper Ollama API Binding

This repository provides an F# [WebSharper](https://websharper.com/) binding for the [Ollama API](https://ollama.com/), enabling seamless integration of Ollama's AI-powered tools and services into WebSharper projects.

## Repository Structure

The repository consists of two main projects:

1. **Binding Project**:
   - Contains the F# WebSharper binding for the Ollama API.

2. **Sample Project**:
   - Demonstrates how to use the Ollama API with WebSharper syntax.

## Features

- WebSharper bindings for the Ollama API.
- Example usage of Ollama's AI tools and services.
- Designed for easy integration into WebSharper applications.

## Installation and Building

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- Node.js and npm (for building web assets).
- WebSharper tools.

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/dotnet-websharper/ollama.git
   cd ollama
   ```

2. Build the Binding Project:

   ```bash
   dotnet build WebSharper.Ollama/WebSharper.Ollama.fsproj
   ```

3. Build and Run the Sample Project:

   ```bash
   cd WebSharper.Ollama.Sample
   dotnet build
   dotnet run
   ```

## Why Use the Ollama API

The Ollama API provides advanced AI-powered services, enabling developers to:

1. **Enhance Applications**: Integrate powerful AI functionalities such as natural language processing and machine learning into your applications.
2. **Streamline Development**: Access Ollama's ready-to-use AI tools without the need to build complex AI models from scratch.
3. **Scalable Solutions**: Leverage Ollama's infrastructure for scalable AI-powered services.

With WebSharper bindings, you can harness the capabilities of the Ollama API in your F# projects with minimal effort.

## How to Use the Ollama API

### Example Usage

Below is an example of how to use the Ollama API in a WebSharper project:

```fsharp
open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.Ollama

// Define the connection to the HTML template
// The IndexTemplate binds to the "wwwroot/index.html" file and allows dynamic interaction with the UI
type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

[<JavaScript>]
module Client =
    // Variable to store the AI response from the Ollama API
    let ChatResponse = Var.Create ""

    // Create an instance of the Ollama client, specifying the API host configuration
    let Ollama = new Ollama(Config(host = "http://localhost:5555"))

    // Function to demonstrate the Generate API call
    let GenerateTest() = promise {
        // Create a GenerateRequest with the specified model and prompt
        let request = GenerateRequest(
            model = "llama3.1",
            prompt = "Why is the sky blue?"
        )   
        let! response = Ollama.Generate(request)

        return response;
    }
    
    // Function to demonstrate the Chat API call
    let ChatTest() = promise {
        // Create a ChatRequest with a user message
        let request = ChatRequest(
            model = "llama3.1",
            Messages = [|Message(role = "user", content = "Why is the sky blue?")|]
        )   
        let! response = Ollama.Chat(request)

        return response;
    }

    [<SPAEntryPoint>]
    let Main () =
         // Initialize the main UI logic
        IndexTemplate.Main()
            .Generate(fun _ -> 
                async {
                    // Trigger the GenerateTest function asynchronously
                    return! GenerateTest().Then(fun response -> printfn $"Response: {response.Response}").AsAsync()
                }
                |> Async.Start
            )
            .Chat(fun _ -> 
                async {
                    // Trigger the ChatTest function asynchronously and update the ChatResponse variable
                    return! ChatTest().Then(fun response -> 
                    Var.Set ChatResponse <| response.Message.Content
                    printfn $"Response: {response.Message.Content}").AsAsync()
                }
                |> Async.Start
            )
            // Bind the ChatResponse variable to the UI
            .ChatResponse(ChatResponse.V)
            .Doc()
        |> Doc.RunById "main"

```

This example demonstrates how to send a request to the Ollama API and display the response in a WebSharper application.
