create TABLE IF NOT exists person (
	id int(20) not null primary key auto_increment,
	first_name varchar(80) not null,
	last_name varchar(80) not null
	address varchar(100) not null,
	gender varchar(10) not null,
);