# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY app/src/SwarmMvc/ ./SwarmMvc/
RUN dotnet restore SwarmMvc/SwarmMvc.csproj
RUN dotnet publish SwarmMvc/SwarmMvc.csproj -c Release -o /out

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet","SwarmMvc.dll"]