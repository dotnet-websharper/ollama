namespace WebSharper.Ollama

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =
    let Options =
        Pattern.Config "Options" {
            Required = [
                "numa", T<bool>
                "num_ctx", T<int>
                "num_batch", T<int>
                "num_gpu", T<int>
                "main_gpu", T<int>
                "low_vram", T<bool>
                "f16_kv", T<bool>
                "logits_all", T<bool>
                "vocab_only", T<bool>
                "use_mmap", T<bool>
                "use_mlock", T<bool>
                "embedding_only", T<bool>
                "num_thread", T<int>
            ]
            Optional = [
                "num_keep", T<int>
                "seed", T<int>
                "num_predict", T<int>
                "top_k", T<int>
                "top_p", T<int>
                "tfs_z", T<int>
                "typical_p", T<int>
                "repeat_last_n", T<int>
                "temperature", T<int>
                "repeat_penalty", T<int>
                "presence_penalty", T<int>
                "frequency_penalty", T<int>
                "mirostat", T<int>
                "mirostat_tau", T<int>
                "mirostat_eta", T<int>
                "penalize_newline", T<bool>
                "stop", !|T<string>
            ]
        }

    let ToolCallFunction = 
        Pattern.Config "ToolCallFunction" {
            Required = [
                "name", T<string>
                "arguments", T<obj>
            ]
            Optional = []
        }

    let ToolCall =
        Pattern.Config "ToolCall" {
            Required = [
                "function", ToolCallFunction.Type
            ]
            Optional = []
        }

    let Message =
        Pattern.Config "Message" {
            Required = [
                "role", T<string>
                "content", T<string>
            ]
            Optional = [
                "images", !|T<string> + !|T<Uint8Array>
                "tool_calls", !| ToolCall
            ]
        }

    let ToolFunctionParameters = 
        Pattern.Config "Tool.function.parameters" {
            Required = [
                "type", T<string>
                "required", !|T<string>
                "properties", T<obj>
            ]
            Optional = []
        }

    let ToolFunction = 
        Pattern.Config "Tool.function" {
            Required = [
                "name", T<string>
                "description", T<string>
                "parameters", ToolFunctionParameters.Type
            ]
            Optional = []
        }

    let Tool =
        Pattern.Config "Tool" {
            Required = [
                "type", T<string>
                "function", ToolFunction.Type
            ]
            Optional = []
        }

    let ChatRequest = 
        Pattern.Config "ChatRequest" {
            Required = [ "model", T<string> ]
            Optional = [
                "messages", !|Message
                "stream", T<bool>
                "format", T<string>
                "keep_alive", T<string> + T<int>
                "tools", !|Tool
                "options", !?Options
            ]
        }

    let PullRequest = 
        Pattern.Config "PullRequest" {
            Required = [ "model", T<string> ]
            Optional = [
                "insecure", T<bool>
                "stream", T<bool>
            ]
        }

    let PushRequest = 
        Pattern.Config "PushRequest" {
            Required = [ "model", T<string> ]
            Optional = [
                "insecure", T<bool>
                "stream", T<bool>
            ]
        }

    let CreateRequest = 
        Pattern.Config "CreateRequest" {
            Required = [ "model", T<string> ]
            Optional = [
                "path", T<string>
                "modelfile", T<string>
                "quantize", T<string>
                "stream", T<bool>
            ]
        }

    let DeleteRequest = 
        Pattern.Config "DeleteRequest" {
            Required = [ "model", T<string> ]
            Optional = []
        }

    let CopyRequest = 
        Pattern.Config "CopyRequest" {
            Required = [
                "source", T<string>
                "destination", T<string>
            ]
            Optional = []
        }

    let ShowRequest = 
        Pattern.Config "ShowRequest" {
            Required = [ "model", T<string> ]
            Optional = [
                "system", T<string>
                "template", T<string>
                "options", !?Options
            ]
        }

    let EmbedRequest = 
        Pattern.Config "EmbedRequest" {
            Required = [
                "model", T<string>
                "input", T<string> + !|T<string>
            ]
            Optional = [
                "truncate", T<bool>
                "keep_alive", T<string> + T<int>
                "options", !?Options
            ]
        }

    let EmbeddingsRequest = 
        Pattern.Config "EmbeddingsRequest" {
            Required = [
                "model", T<string>
                "prompt", T<string>
            ]
            Optional = [
                "keep_alive", T<string> + T<int>
                "options", !?Options
            ]
        }

    let GenerateRequest =
        Pattern.Config "GenerateRequest" {
            Required = [
                "model", T<string>
                "prompt", T<string>
            ]
            Optional = [
                "suffix", T<string>
                "system", T<string>
                "template", T<string>
                "context", !|T<int>
                "stream", T<bool>
                "raw", T<bool>
                "format", T<string>
                "images", !|T<Uint8Array> + !|T<string>
                "keep_alive", T<string> + T<int>
                "options", !?Options
            ]
        }

    let GenerateResponse = 
        Pattern.Config "GenerateResponse" {
            Required = [
                "model", T<string>
                "created_at", T<Date>
                "response", T<string>
                "done", T<bool>
                "done_reason", T<string>
                "context", !|T<int>
                "total_duration", T<int>
                "load_duration", T<int>
                "prompt_eval_count", T<int>
                "prompt_eval_duration", T<int>
                "eval_count", T<int>
                "eval_duration", T<int>
            ]
            Optional = []
        }

    let ChatResponse = 
        Pattern.Config "ChatResponse" {
            Required = [
                "model", T<string>
                "created_at", T<Date>
                "message", Message.Type
                "done", T<bool>
                "done_reason", T<string>
                "total_duration", T<int>
                "load_duration", T<int>
                "prompt_eval_count", T<int>
                "prompt_eval_duration", T<int>
                "eval_count", T<int>
                "eval_duration", T<int>
            ]
            Optional = []
        }

    let EmbedResponse = 
        Pattern.Config "EmbedResponse" {
            Required = [
                "model", T<string>
                "embeddings", !| !|T<int>
            ]
            Optional = []
        }

    let EmbeddingsResponse = 
        Pattern.Config "EmbeddingsResponse" {
            Required = [
                "embedding", !|T<int>
            ]
            Optional = []
        }

    let ProgressResponse = 
        Pattern.Config "ProgressResponse" {
            Required = [
                "status", T<string>
                "digest", T<string>
                "total", T<int>
                "completed", T<int>
            ]
            Optional = []
        }

    let ModelDetails = 
        Pattern.Config "ModelDetails" {
            Required = [
                "parent_model", T<string>
                "format", T<string>
                "family", T<string>
                "families", !|T<string>
                "parameter_size", T<string>
                "quantization_level", T<string>
            ]
            Optional = []
        }

    let ModelResponse = 
        Pattern.Config "ModelResponse" {
            Required = [
                "name", T<string>
                "modified_at", T<Date>
                "size", T<int>
                "digest", T<string>
                "details", !|ModelDetails.Type
                "expires_at", T<Date>
                "size_vram", T<int>
            ]
            Optional = []
        }    

    let ShowResponse = 
        Pattern.Config "ShowResponse" {
            Required = [
                "license", T<string>
                "modelfile", T<string>
                "parameters", T<string>
                "template", T<string>
                "system", T<string>
                "details", ModelDetails.Type
                "messages", !|Message
                "modified_at", T<Date>
                "model_info", T<obj>
            ]
            Optional = [
                "projector_info", T<obj>
            ]
        }

    let ListResponse = 
        Pattern.Config "ListResponse" {
            Required = [
                "models", !|ModelResponse
            ]
            Optional = []
        }

    let ErrorResponse = 
        Pattern.Config "ErrorResponse" {
            Required = [
                "error", T<string>
            ]
            Optional = []
        }

    let StatusResponse = 
        Pattern.Config "StatusResponse" {
            Required = [
                "status", T<string>
            ]
            Optional = []
        }

    let Ollama = 
        Class "Ollama" 
        |+> Instance [
            "chat" => ChatRequest?request ^-> T<Promise<_>>[ChatResponse]
            "generate" => GenerateRequest?request ^-> T<Promise<_>>[GenerateResponse]
            "pull" => PullRequest?request ^-> T<Promise<_>>[ProgressResponse]
            "push" => PushRequest?request ^-> T<Promise<_>>[ProgressResponse]
            "create" => CreateRequest?request ^-> T<Promise<_>>[ProgressResponse]
            "delete" => DeleteRequest?request ^-> T<Promise<_>>[StatusResponse]
            "copy" => CopyRequest?request ^-> T<Promise<_>>[StatusResponse]
            "list" => T<unit> ^-> T<Promise<_>>[ListResponse]
            "show" => ShowRequest?request ^-> T<Promise<_>>[ShowResponse]
            "embed" => EmbedRequest?request ^-> T<Promise<_>>[EmbedResponse]
            "embeddings" => EmbeddingsRequest?request ^-> T<Promise<_>>[EmbeddingsResponse]
            "ps" => T<unit> ^-> T<Promise<_>>[ListResponse]
            "abort" => T<unit> ^-> T<Promise<unit>>
        ]

    let Assembly =
        Assembly [
            Namespace "WebSharper.Ollama" [
                 
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
