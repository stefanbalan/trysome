#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0.0-preview.3 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0.100-preview.3 AS build
ARG VERSION=1.0.0
WORKDIR /src
COPY ["CoL.Service/CoL.Service.csproj", "CoL.Service/"] 
COPY ["CoL.DB/CoL.DB.csproj", "CoL.Service/"] 
RUN dotnet restore "CoL.Service/CoL.Service.csproj" 
COPY . .
WORKDIR "/src/CoL.Service" 
#RUN dotnet tool restore 
#RUN dotnet ef migrations add InitialCreate --project /src/CoL.DB/CoL.DB.csproj --startup-project /src/CoL.Service/CoL.Service.csproj 
RUN dotnet build "CoL.Service.csproj" -c Release -o /app/build /p:Version=$VERSION

FROM build AS publish 
RUN dotnet publish "CoL.Service.csproj" -c Release -o /app/publish
#COPY --from=build /src/CoL.Service/appsettings.Docker.json /data/appsettings.json

FROM base AS final
WORKDIR /app 
COPY --from=publish /app/publish .
# Expose /data directory 
VOLUME /data 
ENTRYPOINT ["dotnet", "CoL.Service.dll"]