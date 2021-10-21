#Obten a imaxe base do SDK de Microsoft
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
# Directorio de traballo para a API
WORKDIR /app

# Copia o arquivo CSPROJ do noso PC a un container Docker e recupera calquera dependencia a traves de NUGET
COPY *.csproj ./
RUN dotnet restore

# Copia os arquivos do proxecto e construe o release
COPY . ./
# Lanzamos dotnet publish coa flag de configuracion para release e o output (-o) para o que e basicamente o proxecto compilado nunha carpeta chamada out
RUN dotnet publish -c Release -o out 

# Xenera o runtime da imaxe Docker
# Un container mellor que sexa pequeno e eficiente, non queremos usar todo o dotnet core SDK porque solo se require para actividades de construccion da imaxe, pero non para a app, co cal aqui usamos a linha con aspnet e non o sdk completo
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
# Exponhemos un porto, indicamos algo como "temos un contenedor" "e queremos que este exposto no porto 80 para o seu uso"
EXPOSE 80
#Esta linha e a multi-part building e basicamente combina partes dos pasos da build previa e ponas no noso directorio de traballo
COPY --from=build-env /app/out .
# Indica como queremos que arranque o noco container, como comeza e que vai facer.
#Neste caso o que fai e simplemente usar o comando dotnet para lanzar a nosa DLL, que efectivamente e neste caso a nosa app
ENTRYPOINT ["dotnet", "Chulygon.dll"]

#Na linea de comandos 
# docker build -t linovi27/dockerprobasapi .
#tamen se pode escribir
#docker build -t linovi27/dockerprobasapi:versiondedocker .
#se sabes a version exacta de docker, pero non fai falta

#dockerignore evita crear os directotios /bin o /obj tipicos de Linux para facer o container moito mais eficientes. Hai dockers en formato Linux ou formato Windows