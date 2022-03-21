# Desafio Let's code

Execução BACKEND padrão com dotnet 6 - mas usando https na 5001 ;)

FRONTEND retirado do exemplo, apenas com pequenas adaptações.

## Backend 

Instalar [.net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) 

Executar na pasta do BACK:
```
dotnet run LetsCode.csproj
```

Ele ouvirá na porta 5000 e "certificado" na porta 5001 (confirir na [documentação da microsoft](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-6.0&tabs=visual-studio#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos) como confiar no certificado de desenvolvimento).  
&nbsp;  
### Considerações
  
Foram dedicadas 5h para este desenvolvimento, e ele não comporta quase nenhuma boa prática para desenvolvimento, principalmente organização de código.  

Foi usado `FluentValidation` para validar o dto recebido do frontend, uma técnica de repositório para os usuários fake da aplicação, isolamento dos modelos de negócio dos expostos na API, e alguma coisa de criação de constantes para evitar `hard code`.  

Porém poderia ser melhor organizado colocando um pouco de DDD, separando as regras de negócio do controller para o domínio, até mesmo criando mais projetos na solução para uma separação de camadas.

Optei por utilizar as bibliotecas nativas do .net para validação do JWT com sugestão padrão de política.
&nbsp;  

## Frontend

Foi modificado para trabalhar com https na porta 5001, além de pequenas alterações visuais.