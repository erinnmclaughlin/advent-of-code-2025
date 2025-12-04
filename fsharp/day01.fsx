open System.IO

let processLine (currentPos: int) (line: string) : int * int =
    let increment = if line[0] = 'L' then -1 else 1
    [ 1 .. line[1..] |> int ]
    |> List.fold 
       (
           fun (cur, pass) _ ->
           let next = (currentPos + increment + 100) % 100
           let pass' = if next = 0 then pass + 1 else pass
           next, pass'
       )
       (currentPos, 0)

let filePath =
    match fsi.CommandLineArgs |> Array.skip 1 with
    | [| fp |] -> fp
    | _ -> System.IO.Path.Combine("..", "inputs", "day01.txt")

printfn "%s" filePath

let _, exactlyZero, passThroughZero = 
    File.ReadLines filePath
    |> Seq.fold
        (fun (dialPos, exactCount, passCount) line ->
            let dialPosAfter, passDelta = processLine dialPos line
            let exactCountDelta = if dialPosAfter = 0 then 1 else 0
            dialPosAfter, exactCount + exactCountDelta, passCount + passDelta
        )
        (50, 0, 0)

printfn "Part one: %i" exactlyZero
printfn "Part two: %i" passThroughZero
