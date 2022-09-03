# Data Transfer Objects manager
We are interested in cool name for this tool (like prisma for orm)

## WHY

All dev world wants typesafety between frontend and backend. There are options like tRCP or GraphQl. But they still have limits. tRCP lock you with typescript, GraphQl needs definition of schema both in the frontend and in the backend.
But idea of typesafety for us is when you have one file for model of data transfer object, that is'n connected to lang. And you can use it one file (model) in any frontend and backend.


## HOW

We will create tool, that will translate .dto file into files for specific lang.


## WHAT

We plan make cli and "daemon". With cli you can add new dtos, bind it to specifil folders, langs. Also I think about workspaces to seperate dtos for each project. "Daemon" will see changes in .dto files and retranslate all binded files to seted langs.