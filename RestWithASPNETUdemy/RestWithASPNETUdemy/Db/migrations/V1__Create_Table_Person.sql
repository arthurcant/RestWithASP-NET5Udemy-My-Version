create TABLE IF NOT exists person (
	id int(20) not null primary key auto_increment,
	first_name varchar(80) not null,
	last_name varchar(80) not null
	address varchar(100) not null,
	gender varchar(10) not null,
);

INSERT INTO person(id, address, first_name, gender, last_name) VALUES
(1, 'São Paulo - Brasil', 'Ayrton01', 'Male', 'Sennar')
(2, 'São Paulo - Brasil', 'Ayrton02', 'Male', 'Sennar')
(3, 'São Paulo - Brasil', 'Ayrton03', 'Male', 'Sennar')
(4, 'São Paulo - Brasil', 'Ayrton04', 'Male', 'Sennar')
(5, 'São Paulo - Brasil', 'Ayrton05', 'Male', 'Sennar')

