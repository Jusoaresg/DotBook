# DotBook - Projeto de estudo

O DotBook é uma loja online de livros desenvolvida em ASP.NET Core MVC.

## Deploy

O DotBook esta atualmente implantado e acessivel online. Você pode visitar a aplicação no link:

https://dotbook.onrender.com


## Visão Geral

### O DotBook é uma aplicação simples, mas abrangente, que utiliza diversas tecnologias e boas práticas de desenvolvimento. Aqui estão alguns destaques do projeto:

ASP.NET Core MVC: Utilizando o framework web da Microsoft para criar uma aplicação robusta e escalável.

Entity Framework Core: Implementando um sistema de banco de dados PostgreSQL para gerenciar produtos e categorias de forma eficiente.

Identity Roles: Separando as áreas de Customer e Admin para garantir funcionalidades distintas para diferentes usuários.

Makefile: Automatizando tarefas comuns do projeto para simplificar o fluxo de trabalho.

## Configuração do Ambiente no Linux
Para configurar o ambiente de desenvolvimento:

Clone o repositório:

```bash
git clone https://github.com/jusoaresg/DotBook
cd DotBook
```

Inicie o Docker-Compose com o DotBook e o PostgreSQL

```bash
sudo docker-compose up
```

# Estrutura do Projeto
O projeto está organizado em diferentes diretórios para manter uma estrutura clara e modular:

Models: Modelos de classes. <br/>
DataAccess: Configurações e inicialização do banco de dados. <br/>
Areas: Contém áreas separadas para Customer e Admin. <br/>
Controllers, Models, Views: Componentes principais da arquitetura MVC. <br/>
Makefile: Arquivo para automação das tarefas do docker e dotnet CLI. <br/>

## Funcionalidades
Áreas Separadas: Diferentes funções para usuários, com o Admin tendo acesso a páginas de criação de categorias e produtos.

CRUD de Produtos: Possibilidade de criar, ler, atualizar e excluir produtos.

CRUD de Categorias: O Admin pode gerenciar categorias, adicionando novas ou removendo existentes.

Carrinho de compras: Funcionalidades de deletar, incrementar e decrementar quantidade.