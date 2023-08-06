#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CoL.Service/CoL.Service.csproj", "CoL.Service/"]
COPY ["CoL.DB/CoL.DB.csproj", "CoL.Service/"]
RUN dotnet restore "CoL.Service/CoL.Service.csproj"
COPY . .
WORKDIR "/src/CoL.Service"
#RUN dotnet tool restore
#RUN dotnet ef migrations add InitialCreate --project /src/CoL.DB/CoL.DB.csproj --startup-project /src/CoL.Service/CoL.Service.csproj
RUN dotnet build "CoL.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoL.Service.csproj" -c Release -o /app/publish
COPY --from=build /src/CoL.Service/appsettings.Docker.json /data/appsettings.json 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME /data # Expose /data directory
ENTRYPOINT ["dotnet", "CoL.Service.dll"]
CMD ["dotnet", "CoL.Service.dll", "files"] CMD ["dotnet", "CoL.Service.dll"]
