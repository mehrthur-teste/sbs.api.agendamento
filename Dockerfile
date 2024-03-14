#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:8001;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["cd/4-Apresentacao/sbs.api.agendamento.webapi/sbs.api.agendamento.webapi.csproj", "cd/4-Apresentacao/sbs.api.agendamento.webapi/"]
COPY ["cd/5-CrossCutting/sbs.api.agendamento.dependencyinjection/sbs.api.agendamento.dependencyinjection.csproj", "cd/5-CrossCutting/sbs.api.agendamento.dependencyinjection/"]
COPY ["cd/1-Aplicacao/sbs.api.agendamento.aplicacao/sbs.api.agendamento.aplicacao.csproj", "cd/1-Aplicacao/sbs.api.agendamento.aplicacao/"]
COPY ["cd/2-Dominio/sbs.api.agendamento.dominio/sbs.api.agendamento.dominio.csproj", "cd/2-Dominio/sbs.api.agendamento.dominio/"]
COPY ["cd/3-Infra/sbs.api.agendamento.repository/sbs.api.agendamento.repository.csproj", "cd/3-Infra/sbs.api.agendamento.repository/"]
RUN dotnet restore "cd/4-Apresentacao/sbs.api.agendamento.webapi/sbs.api.agendamento.webapi.csproj"
COPY . .
WORKDIR "/src/cd/4-Apresentacao/sbs.api.agendamento.webapi"
RUN dotnet build "sbs.api.agendamento.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sbs.api.agendamento.webapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sbs.api.agendamento.webapi.dll"]