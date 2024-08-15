-- Professeur
DO $$ 
BEGIN
    FOR i IN 1..30 LOOP
        INSERT INTO Professeur (nom, prenom, genre) VALUES (
            'Nom' || i, 
            'Prenom' || i, 
            CASE WHEN i % 2 = 0 THEN 'Male' ELSE 'Female' END
        );
    END LOOP;
END $$;

-- Matiere
DO $$ 
BEGIN
    FOR i IN 1..70 LOOP
        INSERT INTO Matiere (nom) VALUES ('Matiere' || i);
    END LOOP;
END $$;

-- Professeur_Matiere
DO $$ 
BEGIN
    FOR i IN 1..20 LOOP
        INSERT INTO Professeur_Matiere (professeur_id, matiere_id) VALUES (
            i, 
            (i % 70) + 1
        );
    END LOOP;
END $$;

-- Classe
DO $$ 
BEGIN
    FOR i IN 1..7 LOOP
        INSERT INTO Classe (niveau, professeur_id) VALUES (
            'Niveau' || (i), 
            (i % 30) + 1
        );
    END LOOP;
END $$;

-- Eleve
DO $$ 
BEGIN
    FOR i IN 1..100 LOOP
        INSERT INTO Eleve (nom, prenom, genre, classe_id) VALUES (
            'EleveNom' || i, 
            'ElevePrenom' || i, 
            CASE WHEN i % 2 = 0 THEN 'Male' ELSE 'Female' END,
            random() * 6 + 1
        );
    END LOOP;
END $$;

-- Note
DO $$ 
BEGIN
    FOR i IN 1..100 LOOP
        INSERT INTO Note (valeur, eleve_id, matiere_id) VALUES (
            (random() * 20)::double precision, 
            (i % 100) + 1, 
            (i % 70) + 1
        );
    END LOOP;
END $$;
