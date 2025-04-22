(*
  bash $ dotnet fsi
  > #r "nuget: DuckDB.NET.Data.Full";;
  > #quit;;
  bash $ # tree ~/.nuget
  bash $ ln -s ~/.nuget/packages/duckdb.net.bindings.full/1.2.1/runtimes/linux-x64/native/libduckdb.so ~/.nuget/packages/duckdb.net.bindings.full/1.2.1/lib/net8.0/libduckdb.so
  bash $ LD_LIBRARY_PATH=~/.nuget/packages/duckdb.net.bindings.full/1.2.1/lib/net8.0 dotnet fsi --load:tst-duckdb.fsx
  >
*)
#r "nuget: DuckDB.NET.Data.Full"
open DuckDB
open DuckDB.NET
open DuckDB.NET.Data

let con = new DuckDBConnection("DataSource=:memory:")
let cmd = new DuckDBCommand("with _ as (select unnest(generate_series(1,5)) as r) select r,r*r as r2 from _", con)
con.Open ()
let reader = cmd.ExecuteReader()
let res = seq {
            while reader.Read() do
              let f1 = reader.GetProviderSpecificValue(0)
              let f2 = reader.GetProviderSpecificValue(1)
              (* printfn "%A, %A" f1 f2 *)
              yield (f1,f2)
            }
let printer (a,b) = printfn "%A, %A" a b
(*
res |> Seq.iter printer
*)
let res_list = Seq.toList res
