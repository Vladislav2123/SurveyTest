FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentation/SurveyTest.API/SurveyTest.API.csproj", "Presentation/SurveyTest.API/"]
COPY ["src/Core/SurveyTest.Application/SurveyTest.Application.csproj", "Core/SurveyTest.Application/"]
COPY ["src/Core/SurveyTest.Domain/SurveyTest.Domain.csproj", "Core/SurveyTest.Domain/"]
COPY ["src/Infrastructure/SurveyTest.DAL/SurveyTest.DAL.csproj", "Infrastructure/SurveyTest.DAL/"]
RUN dotnet restore "Presentation/SurveyTest.API/SurveyTest.API.csproj"
COPY ./src .
WORKDIR "/src/Presentation/SurveyTest.API"
RUN dotnet publish "SurveyTest.API.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet", "SurveyTest.API.dll"]

EXPOSE 5000
EXPOSE 5001