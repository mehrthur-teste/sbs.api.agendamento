# Introduction executar os seguintes comandos
#prompt
docker image build --no-cache -t apoioprodesp/agendamento-consulado:v1 . 
docker run -d --name a-c -p 8001:80 apoioprodesp/agendamento-consulado:v1
docker push apoioprodesp/agendamento-consulado:v1
#Browser
http://localhost:8001/swagger/index.html