update db_info set version = '1.2' where id = 1;

drop view book_data;
drop view loaned_book_data;
drop view loaned_book_records;
drop view wish_list_data;

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