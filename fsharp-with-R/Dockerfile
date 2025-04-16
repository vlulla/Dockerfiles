# syntax=docker/dockerfile:1
ARG VERSION=latest
FROM rocker/r-base:${VERSION}
ARG VERSION
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-eux", "-o", "pipefail", "-c"]

## Useful tools for all of my exploratory work.
RUN <<EOT
set -ex
export DEBIAN_FRONTEND=noninteractive
apt-get update -qq --yes

apt-get install --yes --auto-remove --no-install-recommends build-essential ca-certificates tini coreutils findutils git tree jq htop vim ripgrep curl gnupg strace psmisc iputils-ping tcpdump traceroute procps

apt-get autoclean && rm -rf /var/lib/apt/lists/*
unset DEBIAN_FRONTEND
EOT

RUN <<EOT
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
apt-get update -qq --yes
apt-get upgrade --yes
apt-get install --yes dotnet-sdk-9.0
ln -s /usr/lib/x86_64-linux-gnu/libdl.so{.2,}
EOT

ARG USR=${USR:-usr}
ARG GRP=${GRP:-grp}

## ## See the rules that ADD obeys at https://docs.docker.com/engine/reference/builder/#add
## ## <src> path must be **inside of the context** of the build! And, that is why I cannot do ADD --chown=${USR}:${GRP} /home/vijay /home/usr !
##
## ADD --chown=${USR}:${GRP} . /home/usr

RUN <<EOT
set -ex
adduser --group ${GRP}
adduser --shell /bin/bash --ingroup ${GRP} ${USR}
EOT

WORKDIR /home/${USR}
USER ${USR}

RUN <<EOT
>tst.fsx cat <<EOF
(*
  See https://fslab.org/RProvider/tutorial.html

  bash $ R_HOME=$(R RHOME) dotnet fsi --load:tst.fsx
*)
#r "nuget: RProvider"
open RProvider
open RProvider.stats
open System

fsi.AddPrinter FSIPrinters.rValue

EOF
EOT

RUN <<EOT
>lm_eg.fsx cat <<EOF
(*
  See https://fslab.org/RProvider/quickstart-statistics.html

  bash $ R_HOME=$(R RHOME) dotnet fsi --load:lm_eg.fsx
*)
#r "nuget: RProvider"
open RProvider
open RProvider.stats
open RDotNet
open System

fsi.AddPrinter FSIPrinters.rValue

let rng = System.Random()
let rand () = rng.NextDouble()

let X1s = [ for i in 0 .. 9 -> 10. * rand ()]
let X2s = [ for i in 0 .. 9 -> 5. * rand ()]

let Ys = [ for i in 0 .. 9 -> 5. + 3. * X1s.[i] - 2. * X2s.[i] + rand ()]

let dataset = [
  "Y" => Ys
  "X1" => X1s
  "X2" => X2s ] |> R.data_frame

let result = R.lm(formula="Y~X1+X2",data=dataset)
let coefficients = result.AsList().["coefficients"].AsNumeric()
let residuals = result.AsList().["residuals"].AsNumeric()
let summary = R.summary(result)
summary.AsList().["r.squared"].AsNumeric()

EOF
EOT
## ## Understanding interaction between ENTRYPOINT and CMD is helpful. https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact
## Might resolve timedatectl/systemd issues that I saw in R console on Vertex AI??
ENTRYPOINT ["tini", "--"]

CMD ["/bin/bash"]

