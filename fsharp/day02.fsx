open System.IO

let parseRange (line: string) =
    line.Split '-' |> fun parts -> [parts[0] |> int .. parts[1] |> int]

let getRepeatedString part count : string =
    Seq.replicate count part |> String.concat ""

let getPart (idString: string) numParts =
    idString.Length / numParts
    |> fun len -> idString[..len-1]

let isPartRepeated (idString: string) numParts : bool =
    getPart idString numParts
    |> fun part -> getRepeatedString part numParts 
    |> idString.Equals

let mustBeValid (idString: string) numParts =
    idString.Length = 1 || idString.Length % numParts <> 0 

let isValid id numParts : bool =
    id
    |> string
    |> fun idString ->
        mustBeValid idString numParts ||
        isPartRepeated idString numParts 
        |> not

let ids = 
    Path.Combine("inputs", "day02.txt")
    |> File.ReadAllText
    |> fun s -> s.Split ','
    |> Seq.collect parseRange
    |> Seq.toArray

ids
|> Seq.filter (fun id -> isValid id 2 |> not)
|> Seq.sum
|> printfn "part one: %i"
