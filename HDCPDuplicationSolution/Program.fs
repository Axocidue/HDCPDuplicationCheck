open System
open System.IO
open System.Text
open System.Text.RegularExpressions

let target = @"C:\Lemon\Personal\Desktop\target.txt"
let source = @"C:\Lemon\Personal\Desktop\source.txt"
let checkfile = @"C:\Lemon\Personal\Desktop\checkfile.txt"

let read file = File.ReadAllLines(file,Encoding.Default)

let parse_target line = 
    let elements = Regex.Split(line,"\t")
    let key = (elements.[0],elements.[1])
    let value = elements.[2]
    (key, value)

let targets = target |> read |> Array.map (fun line -> line |> parse_target)

let parse_targetkey line = 
    let elements = Regex.Split(line,"\t")
    (elements.[0],elements.[1])

let target_keys = target |> read |> Array.map (fun line -> line |> parse_targetkey)

let parse_targetvalue line = 
    let elements = Regex.Split(line,"\t")
    elements.[2]

let target_values = target |> read |> Array.map (fun line -> line |> parse_targetvalue)

let parse_source line = 
    let elements = Regex.Split(line, "\t")
    (elements.[0],elements.[1])

let sources = source |> read |> Array.map (fun line -> line |> parse_source)

let check_results = sources |> Array.map (fun item -> 
    match target_keys |> Array.tryFindIndex (fun i -> i = item) with
    | Some index -> (item, target_values.[index])
    | None -> (item, "???") ) 


let build_line result = 
    let (model, si), project = result
    model + "\t" + si + "\t" + project + "\r\n"

let build_text = check_results |> Array.map (fun line -> line |> build_line) |> Array.fold (+) ""

let write file = File.WriteAllText(file, build_text, Encoding.Default)

write checkfile