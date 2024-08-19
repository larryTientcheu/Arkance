/*==============================================================*/
/* Table: Professeur                                            */
/*==============================================================*/
create table Professeur (
    id int primary key generated always as identity,
    nom text not null,
    prenom text not null,
    genre varchar(50) not null
);

/*==============================================================*/
/* Table: Matiere                                               */
/*==============================================================*/
create table Matiere (
    id int primary key generated always as identity,
    nom text not null
);

/*==============================================================*/
/* Table: professeur_matiere                                    */
/*==============================================================*/
create table professeur_matiere (
    professeur_id int references Professeur (id) on delete restrict on update restrict,
    matiere_id int references Matiere (id) on delete restrict on update restrict,
    primary key (professeur_id, matiere_id)
);

create  index idx_professeur_matiere_professeur_id on professeur_matiere (
professeur_id
);

create  index idx_professeur_matiere_matiere_id  on professeur_matiere (
matiere_id
);

/*==============================================================*/
/* Table: Classe                                                */
/*==============================================================*/
create table Classe (
    id int primary key generated always as identity,
    niveau varchar(50) not null,
    professeur_id int references Professeur (id) on delete restrict on update restrict
);

create  index idx_classe_professeur_id on Classe (
professeur_id
);

/*==============================================================*/
/* Table: Eleve                                                 */
/*==============================================================*/
create table Eleve (
    id int primary key generated always as identity,
    nom text not null,
    prenom text not null,
    genre varchar(50) not null,
    classe_id int references Classe (id) on delete restrict on update restrict
);

create  index idx_eleve_classe_id on Eleve (
classe_id
);

/*==============================================================*/
/* Table: Note                                                  */
/*==============================================================*/
create table Note (
    id int primary key generated always as identity,
    valeur double precision check (valeur is null or (valeur between 0 and 20)),
    eleve_id int references Eleve (id) on delete cascade on update cascade,
    matiere_id int references Matiere (id) on delete restrict on update restrict,
    appreciation varchar(25) not null,
    unique (eleve_id, matiere_id)
);

create index idx_note_eleve_id_matiere_id on Note (eleve_id, matiere_id);

create  index idx_note_eleve_id on Note (
eleve_id
);

create  index idx_note_matiere_id on Note (
matiere_id
);

