open System.IO

let parseInstruction (instruction: string) =
    let ticks = instruction[1..] |> int
    if instruction[0] = 'L' then -ticks else ticks

let toDialPos rawValue =
    let m = rawValue % 100
    if m < 0 then m + 100 else m

let getDiv a = if a >= 0 then a / 100 else (a - 99) / 100

let solve (pos, partOne, partTwo) instruction =
    let delta = parseInstruction instruction
    let nextPosRaw = pos + delta
    let nextPos = toDialPos nextPosRaw

    let partOneIncrement = if nextPos = 0 then 1 else 0
    let partTwoIncrement =
        if delta > 0 then
            getDiv nextPosRaw
        elif delta < 0 then
            getDiv (pos - 1) - getDiv (nextPosRaw - 1)
        else
            0

    nextPos,
    partOne + partOneIncrement,
    partTwo + partTwoIncrement

let filePath =
    match fsi.CommandLineArgs |> Array.skip 1 with
    | [| fp |] -> fp
    | _ -> Path.Combine("inputs", "day01.txt")

let fileLines = File.ReadLines filePath

fileLines
    |> Seq.fold solve (50, 0, 0)
    |> fun (_, partOne, partTwo) ->
        printfn "part one: %i\npart two: %i" partOne partTwo
