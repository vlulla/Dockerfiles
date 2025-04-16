(*
  See https://fslab.org/RProvider/tutorial.html

  bash $ R_HOME=$(R RHOME) dotnet fsi --load:tst.fsx
*)
#r "nuget: RProvider"
open RProvider
open RProvider.stats
open System

fsi.AddPrinter FSIPrinters.rValue
