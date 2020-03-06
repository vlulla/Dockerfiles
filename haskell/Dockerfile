FROM haskell

ARG USER_ID
ARG GROUP_ID

RUN addgroup --gid ${GROUP_ID} u && useradd --uid ${USER_ID} --gid ${GROUP_ID} u
USER u

## CMD [ "ghci" ]
CMD [ "bash" ]
