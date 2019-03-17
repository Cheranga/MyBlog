FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["MyBlog.Api/MyBlog.Api.csproj", "MyBlog.Api/"]
RUN dotnet restore "MyBlog.Api/MyBlog.Api.csproj"
COPY . .
WORKDIR "/src/MyBlog.Api"
RUN dotnet build "MyBlog.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MyBlog.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyBlog.Api.dll", "--environment=Production"]