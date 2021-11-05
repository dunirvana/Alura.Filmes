# Alura.Filmes
Entity Framework com bancos de dados legados

## Mapeamento de tabelas, indices e retrições unicas
- indices
- restrições UNIQUE

## Restrição CHECK
- o Entity não dá suporte nativo a restrições do tipo CHECK, logo devemos cria-las usando o recurso de Migrations do EF Core, através do método Sql().

## Enumerados
- convenção do Entity para mapeamento de enumerados, que é usar uma coluna do tipo inteiro
- estratégia para mapear valores enumerados em strings com valores específicos (usando a coleção Dictionary)
- alternativas para fazer o Entity ignorar propriedades
- ModelSnapshot, que representa o modelo de dados que o Entity vai usar para mapear os mundos OO e relacional

## Herança
- convenções para mapeamento de heranças no Entity.

## Geração de SQL
- assumindo o controle da geração do SQL através do método FromSql()
- limitações que esse método possui e dicas para superá-las
- como integrar views na aplicação usando o FromSql()

## Stored Procedures
- ocomando ExecuteSqlCommand
- passar parâmetros para o ExecuteSqlCommand
- ler valores de parâmetros recuperados do ExecuteSqlCommand
- enviar comandos INSERT, DELETE e UPDATE
