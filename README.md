# SchoolSystem
Este sistema foi criado para testar minhas habilidades em C#, ASP.NET e Banco de Dados (Sem foco em HTML ou CSS(BootStrap foi utilizado para estilização)), o projeto em si é bem simples, mas tem muitas funcionalidades difíceis de se implementar.

O projeto conta com uma página para Login, uma para a administração, uma para os professores, e outra para os alunos, cada uma com suas respectivas ferramentas, o sistema é feito para organização e acesso a dados, mas não para aulas EAD, pois o sistema não conta com ferramentas de comunicação entre professores e alunos.


## Login e identificação
Na página de Login o usuário deve se identificar, com seu Id de identificação (Cada usuário recebe um quando cria sua conta), seu email, sua senha, e deve informar se quer logar como Professor ou Aluno
![login_admin](https://github.com/user-attachments/assets/3eb7307d-7f35-4dc3-89cb-f0b1a33c5fc4)



## Administração
A área da administração conta com várias ferramentas utilizadas na administração da escola, organização de alunos, professores, salas, aulas e etc.
![menu_admin](https://github.com/user-attachments/assets/9ad68be2-348a-46a6-b3fe-0ec5f88a6337)
A administração tem o poder de lançar novos professores/alunos no banco de dados, e tem acesso a uma tabela mostrando seus dados, cada aluno tem uma turma, e um nome (Além dos dados de usuários e outros dados implícitos no banco de dados), e cada professor tem um nome, e suas competências (Além dos dados de usuários e outros dados implícitos no banco de dados), quando a administração cria um novo professor/aluno ela deve informar todos esses dados.

No momento em que a administração cria um novo professor/aluno, ela deve prover o email e a senha de usuário do professor/aluno, para que ele tenha acesso ao sistema.
![exemplo_admin](https://github.com/user-attachments/assets/cceedede-014d-43e6-a323-b507d87f435a)
![exemplo_admin_professor](https://github.com/user-attachments/assets/fad41c7e-e709-446a-9bdd-0a22765bc40b)

A administração também tem o poder de organizar as aulas de cada turma, tendo acesso a um menu, que informa as aulas referentes a cada dia da semana para cada turma, com dados como horário, matéria e professor. Para adicionar uma aula, a administração deve informar a turma, a matéria, o professor, a sala em que a aula vai ocorrer, o dia da semana e o horário.
![exemplo_admin_aulas](https://github.com/user-attachments/assets/0d83d8fc-da73-4969-ab79-f90b845bd9db)



## Professor
Logando como professor, o usuário tem acesso a ferramentas de exibição a aos trabalhos de seus alunos.
![exemplo_professor](https://github.com/user-attachments/assets/8a7d2459-730e-458a-83e4-62c04b3ad124)

O professor tem acesso às suas aulas (Botão "Minhas Aulas"), horários, turmas, matérias e salas, dividos em dias da semana.
![exemplo_aulas](https://github.com/user-attachments/assets/002897a9-6566-4043-8237-55e142855441)

O professor também tem acesso às suas turmas (Botão "Turmas e Trabalhos"), podendo lançar trabalhos e notas, para isso ele deve selecionar a turma.
![exemplo_escolha_sala](https://github.com/user-attachments/assets/18883944-8c8a-4510-8ab2-23384f6158ad)
Após a seleção, ele terá acesso às provas e trabalhos
![exemplo_turma_selec2](https://github.com/user-attachments/assets/ccf0714b-f2ff-457b-83ba-fc6f84c917a4)
![exemplo_turma_selec1](https://github.com/user-attachments/assets/e3e2bde2-6fd5-49ce-a1ad-0583be40e759)

Para realizar o lançamento das notas de uma prova, o usuário deve selecionar a matéria, e escolher o botão referente ao bimestre da prova, e então o professor deve colocar as notas de cada aluno da turma selecionada da prova lançada.
![exemplo_lanc_prova](https://github.com/user-attachments/assets/3383f70b-adf9-4316-81a6-ebba859a210e)

O professor também pode adicionar trabalhos para a turma, na opção "Adicionar Trabalho".
![exemplo_criacao_trabalho](https://github.com/user-attachments/assets/0b104360-948d-4d9b-89ac-eb83dfe1f2da)

Após o adicionar o trabalho, este irá aparecer listado na página anterior, ao clicar no trabalho, o professor pode especificar as notas de cada aluno no trabalho (Como dito antes, o sistema não foi feito para aulas EAD, ou seja, o professor deve receber os trabalhos em mãos, e depois colocar as notas no sistema, pois ele não pode receber os trabalhos dos alunos diretamente pelo sistema), após colocar as notas, estas ficarão salvas no sistema, então o professor poderá voltar depois e ter acesso às notas de seus alunos no trabalho.
![exemplo_trabalho](https://github.com/user-attachments/assets/deaf11fc-b8d8-4744-bd21-f3bb97f01e8e)


## Aluno
Logando como aluno, o usuário tem acesso às suas aulas e as suas notas.
![exemplo_aluno](https://github.com/user-attachments/assets/2e8a5dbb-b4ea-499b-9242-77baeaf01666)

No botão "Notas", o aluno tem acesso às suas médias de trabalhos e provas de cada matéria, divididas por bimestre.
![exemplo_notas](https://github.com/user-attachments/assets/408eab28-625a-485e-a057-1e0c78bc9b68)

No botão "Horários", o aluno tem acesso às aulas de sua turma em cada dia da semana.
![exemplo_horarios](https://github.com/user-attachments/assets/5359977b-b41f-43ea-8e0c-3e9380fa55fb)
