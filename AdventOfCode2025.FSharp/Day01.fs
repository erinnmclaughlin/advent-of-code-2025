module AdventOfCode2025.FSharp

open System.IO

type Direction =
    | Left
    | Right

type Instruction = {
    Direction: Direction
    Steps: int
}

module Instruction =
    let parse (line: string) : Instruction =
        let dir =
            match line[0] with
            | 'L' -> Left
            | 'R' -> Right
            | c   -> failwithf $"Unexpected direction '%c{c}' in line: %s{line}"
            
        { Direction = dir; Steps = line[1..] |> int }

module Day01Logic =

    let turnDial (currentPos: int) (direction: Direction) : int =
        let step =
            match direction with
            | Left  -> -1
            | Right ->  1
            
        (currentPos + step + 100) % 100
        
    let processLine (currentPos: int) (line: string) : int * int =
        let instruction =
            Instruction.parse line

        let folder (cur, pass) _ =
            let next = turnDial cur instruction.Direction
            let pass' = if next = 0 then pass + 1 else pass
            next, pass'

        [ 1 .. instruction.Steps ]
        |> List.fold folder (currentPos, 0)
    
    let solve (lines: seq<string>) =
        lines
        |> Seq.fold
            (fun (dialPos, exactCount, passCount) line ->
                let dialPosAfter, passDelta = processLine dialPos line
                let exactCountDelta = if dialPosAfter = 0 then 1 else 0
                dialPosAfter, exactCount + exactCountDelta, passCount + passDelta
            )
            (50, 0, 0)

type Day01() =
    interface ISolver<Day01> with
        static member DayNumber = 1;
        member _.Solve(inputFile: FileInfo) : struct (string * string) =
            let _, exact, pass = File.ReadLines inputFile.FullName |> Day01Logic.solve
            struct (string exact, string pass)