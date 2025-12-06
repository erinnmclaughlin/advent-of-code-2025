open System.IO

let digitsToInt64 (digits: int list) =
    digits
    |> List.fold (fun acc d -> acc * 10L + int64 d) 0L

let pickKDigits (k: int) (digits: int array) =
    let n = digits.Length

    let rec loop start remaining acc =
        if remaining = 0 then
            List.rev acc
        else
            let lastIndex = n - remaining

            let mutable bestIndex = start
            let mutable bestDigit = digits[start]

            for i in start + 1 .. lastIndex do
                let d = digits[i]
                if d > bestDigit then
                    bestDigit <- d
                    bestIndex <- i

            loop (bestIndex + 1) (remaining - 1) (bestDigit :: acc)

    loop 0 k []

let filePath =
    match fsi.CommandLineArgs |> Array.skip 1 with
    | [| fp |] -> fp
    | _ -> Path.Combine("inputs", "day03.txt")

let parsedLines = 
    File.ReadLines filePath
    |> Seq.map (fun line ->
        line
        |> Seq.map (fun c -> int c - int '0')
        |> Seq.toArray)
    |> Seq.toArray

let solveFor k =
    parsedLines
    |> Array.sumBy(fun arr ->
        arr
        |> pickKDigits k
        |> digitsToInt64)


printfn "part one: %d" (solveFor 2)
printfn "part two: %d" (solveFor 12)
