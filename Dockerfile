FROM mcr.microsoft.com/dotnet/sdk


WORKDIR .

COPY . .

RUN dotnet build src/TeamFinderAPI.csproj -c Release -o build

FROM mcr.microsoft.com/dotnet/aspnet

WORKDIR ./build

ENTRYPOINT [ "TeamFinderAPI.dll"]