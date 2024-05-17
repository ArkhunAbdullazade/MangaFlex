FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY /MangaFlex.WebApi/MangaFlex.Manga.Api .
RUN dotnet publish -c Release -o dist

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /src/dist .
ENTRYPOINT ["dotnet", "MangaFlex.Manga.Api.dll"]