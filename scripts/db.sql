PRAGMA auto_vacuum = 1;

create table owner_info
(
	id text not null primary key,
	owner_name text not null,
	date_created text,
	last_accessed text,
	db_alias text not null,
	password text
);

create table db_info
(
	id text not null,
	version text not null
);

insert into db_info values ('1','1.1');

create table person
(
	id text primary key not null,
	first_name text,
	last_name text not null
);

create table subject
(
	id text primary key not null,
	subject text not null,
	type text not null
);

create table publisher
(
	id text primary key not null,
	publisher_info text not null
);

create table book
(
	id text not null,
	isbn text,
	short_title text,
	long_title text,
	physical_desc text,
	edition_info text,
	dewey text,
	dewey_norm text,
	lcc_number text,
	publisher_id text,
	summary text,
	notes text,
	awards text,
	date_added text,
	urls text,
	language text,
	type text,
	new_price text,
	used_price text,
	price_paid text,
	PRIMARY KEY (id)
);

create table book_author
(
	book_id text not null,
	person_id text not null
);

create table book_editor
(
	book_id text not null,
	person_id text not null
);

create table book_subject
(
	book_id text not null,
	subject_id text not null
);


create table loan_book
(
	loan_id integer primary key autoincrement,
	book_id text not null,
	loan_to text,
	date_loaned text,
	date_checkedin text
);

create view book_data as select b.*,p.publisher_info AS publisher_info,l.loan_id AS loan_id,l.loan_to AS loan_to,l.date_loaned AS date_loaned,l.date_checkedin AS date_checkedin, s.subject AS subject, pe.last_name AS last_name, pe.first_name AS first_name from book b 
left outer join publisher p on p.id = b.publisher_id 
left outer join loan_book l on l.book_id = b.id
left outer join book_subject bs ON b.id = bs.book_id
left outer join subject s ON bs.subject_id = s.id
left outer join book_author ba ON b.id = ba.book_id
left outer join person pe ON ba.person_id = pe.id;

create view loaned_book_data as select b.*,p.publisher_info AS publisher_info,l.loan_id AS loan_id,l.loan_to AS loan_to,l.date_loaned AS date_loaned,l.date_checkedin AS date_checkedin, s.subject AS subject, pe.last_name AS last_name, pe.first_name AS first_name from book b 
left outer join publisher p on p.id = b.publisher_id 
inner join loan_book l on l.book_id = b.id AND l.date_checkedin=''
left outer join book_subject bs ON b.id = bs.book_id
left outer join subject s ON bs.subject_id = s.id
left outer join book_author ba ON b.id = ba.book_id
left outer join person pe ON ba.person_id = pe.id;

create view loaned_book_records as select b.*,p.publisher_info AS publisher_info,l.loan_id AS loan_id,l.loan_to AS loan_to,l.date_loaned AS date_loaned,l.date_checkedin AS date_checkedin, s.subject AS subject, pe.last_name AS last_name, pe.first_name AS first_name from book b 
left outer join publisher p on p.id = b.publisher_id 
inner join loan_book l on l.book_id = b.id 
left outer join book_subject bs ON b.id = bs.book_id
left outer join subject s ON bs.subject_id = s.id
left outer join book_author ba ON b.id = ba.book_id
left outer join person pe ON ba.person_id = pe.id;

create view wish_list_data as select b.*,p.publisher_info AS publisher_info,s.subject AS subject, pe.last_name AS last_name, pe.first_name AS first_name from book b 
left outer join publisher p on p.id = b.publisher_id 
left outer join book_subject bs ON b.id = bs.book_id
left outer join subject s ON bs.subject_id = s.id
left outer join book_author ba ON b.id = ba.book_id
left outer join person pe ON ba.person_id = pe.id;

create index ind_person_id on person(id);
create index ind_person_fname on person(first_name);
create index ind_person_lname on person(last_name);

create index ind_subject_id on subject(id);
create index ind_subject_sub on subject(subject);
create index ind_subject_type on subject(type);

create index ind_publisher_id on publisher(id);
create index ind_publisher_info on publisher(publisher_info);

create index ind_book_author_bid on book_author(book_id);
create index ind_book_author_pid on book_author(person_id);

create index ind_book_editor_bid on book_editor(book_id);
create index ind_book_editor_pid on book_editor(person_id);

create index ind_book_subject_bid on book_subject(book_id);
create index ind_book_subject_pid on book_subject(subject_id);

create index ind_loan_book_lid on loan_book(loan_id);
create index ind_loan_book_bid on loan_book(book_id);
create index ind_loan_book_dtel on loan_book(date_loaned);
create index ind_loan_book_dtec on loan_book(date_checkedin);

