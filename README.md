# DotBook - Projeto de estudo

O DotBook é uma loja online de livros desenvolvida em ASP.NET Core MVC.

### Pré-Requisitos
**.NET Core SDK 8:** Certifique-se de ter o SDK do .NET Core instalado para compilar e executar o projeto.

## Deploy

O DotBook esta atualmente implantado e acessivel online. Você pode visitar a aplicação no link:

https://dotbook.onrender.com

**OBS**: O site normalmente demora para carregar a primeira vez mesmo, por conta da inatividade de usuarios.

## Tecnologias Utilizadas
**ASP.NET Core MVC:** Framework web da Microsoft para criar uma aplicação robusta
**Entity Framework Core:** Implementação de um sistema de banco de dados PostgreSQL para gerenciar produtos e categorias de forma eficiente.
**Identity Roles:** Separação de áreas de Customer e Admin para garantir funcionalidades distintas para diferentes usuários.

## Visão Geral

### O DotBook é uma aplicação simples, mas abrangente, que utiliza diversas tecnologias e boas práticas de desenvolvimento. Aqui estão alguns destaques do projeto:

ASP.NET Core MVC: Utilizando o framework web da Microsoft para criar uma aplicação robusta e escalável.

Entity Framework Core: Implementando um sistema de banco de dados PostgreSQL para gerenciar produtos e categorias de forma eficiente.

Identity Roles: Separando as áreas de Customer e Admin para garantir funcionalidades distintas para diferentes usuários.

Makefile: Automatizando tarefas comuns do projeto para simplificar o fluxo de trabalho.

## Estrutura do Projeto

**Repository, Unity of Work Pattern:** Utilização de padrões de design para organizar a lógica de acesso a dados.
**Async Methods:** Métodos assíncronos são utilizados, principalmente em operações que envolvem salvar no banco de dados e obtenção de dados.

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

Metodos async: utilizados em metodos que podem comprometer a thread principal