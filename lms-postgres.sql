-- TRUNCATE TABLE t_m_role RESTART IDENTITY CASCADE;

CREATE TABLE t_m_file (
	id SERIAL,
	file_content TEXT NOT NULL,
	file_extension VARCHAR(5) NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
	CONSTRAINT file_pk PRIMARY KEY(id)
);

CREATE TABLE t_m_role (
	id SERIAL,
    role_code VARCHAR(10) NOT NULL,
	role_name VARCHAR(20) NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
	CONSTRAINT role_pk PRIMARY KEY(id),
	CONSTRAINT role_bk UNIQUE(role_code)
);

CREATE TABLE t_m_user (
	id SERIAL,
    full_name VARCHAR(30) NOT NULL,
	email VARCHAR(30) NOT NULL,
	pass TEXT NOT NULL,
	photo_id INT,
	role_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
	CONSTRAINT user_pk PRIMARY KEY(id),
	CONSTRAINT user_email_bk UNIQUE(email),
	CONSTRAINT user_photo_fk FOREIGN KEY(photo_id) REFERENCES t_m_file(id),
	CONSTRAINT user_role_fk FOREIGN KEY(role_id) REFERENCES t_m_role(id)
);

CREATE TABLE t_m_class (
	id SERIAL,
    class_code VARCHAR(10) NOT NULL,
    class_title VARCHAR(50) NOT NULL,
    class_description TEXT,
    class_image_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT class_pk PRIMARY KEY(id),
    CONSTRAINT class_bk UNIQUE(class_code),
	CONSTRAINT class_image_fk FOREIGN KEY(class_image_id) REFERENCES t_m_file(id)
);

CREATE TABLE t_r_student_class (
    id SERIAL,
    class_id INT NOT NULL,
    student_id INT NOT NULL,
    created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT student_pk PRIMARY KEY(id),
    CONSTRAINT student_class_ck UNIQUE(class_id, student_id),
    CONSTRAINT student_class_class_fk FOREIGN KEY(class_id) REFERENCES t_m_class(id),
    CONSTRAINT student_class_student_fk FOREIGN KEY(student_id) REFERENCES t_m_user(id)
);

CREATE TABLE t_m_learning (
	id SERIAL,
	learning_name VARCHAR(30) NOT NULL,
	learning_description TEXT,
    learning_date DATE NOT NULL,
    class_id INT NOT NULL,
    created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT learning_pk PRIMARY KEY(id),
	-- CONSTRAINT learning_ck UNIQUE(learning_date, class_id),
    CONSTRAINT learning_class_fk FOREIGN KEY(class_id) REFERENCES t_m_class(id)
);

CREATE TABLE t_m_session (
	id SERIAL,
    session_name VARCHAR(50) NOT NULL,
    session_description TEXT,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
	learning_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT session_pk PRIMARY KEY(id),
	-- CONSTRAINT session_ck UNIQUE(start_time, end_time, learning_id),
    CONSTRAINT session_learning_fk FOREIGN KEY(learning_id) REFERENCES t_m_learning(id)
);

CREATE TABLE t_m_forum (
	id SERIAL,
	forum_name VARCHAR(30) NOT NULL,
	forum_description TEXT,
	session_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT forum_pk PRIMARY KEY(id),
    CONSTRAINT forum_session_fk FOREIGN KEY(session_id) REFERENCES t_m_session(id)
);

CREATE TABLE t_r_forum_comment (
	id SERIAL,
    comment_content TEXT NOT NULL,
    user_id INT NOT NULL,
    forum_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT forum_comment_pk PRIMARY KEY(id),
    CONSTRAINT forum_comment_user_fk FOREIGN KEY(user_id) REFERENCES t_m_user(id),
    CONSTRAINT forum_comment_forum_fk FOREIGN KEY(forum_id) REFERENCES t_m_forum(id)
);

CREATE TABLE t_r_session_attendance (
	id SERIAL,
	is_approved BOOLEAN NOT NULL,
    student_id INT NOT NULL,
    session_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT session_attendance_pk PRIMARY KEY(id),
	CONSTRAINT session_attendance_ck UNIQUE(student_id, session_id),
    CONSTRAINT session_attendance_student_fk FOREIGN KEY(student_id) REFERENCES t_m_user(id),
    CONSTRAINT session_attendance_session_fk FOREIGN KEY(session_id) REFERENCES t_m_session(id)
);

CREATE TABLE t_m_session_material (
	id SERIAL,
	material_name VARCHAR(30) NOT NULL,
    material_description TEXT,
    session_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT session_material_pk PRIMARY KEY(id),
    CONSTRAINT session_material_fk FOREIGN KEY(session_id) REFERENCES t_m_session(id)
);

CREATE TABLE t_r_session_material_file (
	id SERIAL,
    file_id INT NOT NULL,
    material_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT material_file_pk PRIMARY KEY(id),
    CONSTRAINT material_file_file_fk FOREIGN KEY(file_id) REFERENCES t_m_file(id),
    CONSTRAINT material_file_material_fk FOREIGN KEY(material_id) REFERENCES t_m_session_material(id)
);

CREATE TABLE t_m_task (
	id SERIAL,
	task_name VARCHAR(30) NOT NULL,
	task_description TEXT,
	duration INT NOT NULL,
    session_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT task_pk PRIMARY KEY(id),
    CONSTRAINT task_session_fk FOREIGN KEY(session_id) REFERENCES t_m_session(id)
);

CREATE TABLE t_r_task_file (
	id SERIAL,
    file_name VARCHAR(30),
    file_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT task_file_pk PRIMARY KEY(id),
    CONSTRAINT task_file_fk FOREIGN KEY(file_id) REFERENCES t_m_file(id)
);

CREATE TABLE t_m_task_question (
	id SERIAL,
    question_type VARCHAR(20) NOT NULL,
    question_content TEXT,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
	CONSTRAINT question_pk PRIMARY KEY(id)
);

CREATE TABLE t_m_task_multiple_choice_option (
	id SERIAL,
    option_char CHAR(1) NOT NULL,
    option_text VARCHAR(255),
    is_correct BOOLEAN NOT NULL,
    question_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
	CONSTRAINT multiple_choice_option_pk PRIMARY KEY(id),
    CONSTRAINT multiple_choice_option_char_ck UNIQUE(option_char, question_id),
    CONSTRAINT multiple_choice_option_question_fk FOREIGN KEY(question_id) REFERENCES t_m_task_question(id)
);

CREATE TABLE t_r_task_detail (
	id SERIAL,
	task_id INT NOT NULL,
	task_file_id INT,
	task_question_id INT,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
	CONSTRAINT task_detail_pk PRIMARY KEY(id),
	CONSTRAINT task_detail_task_fk FOREIGN KEY(task_id) REFERENCES t_m_task(id),
	CONSTRAINT task_detail_file_fk FOREIGN KEY(task_file_id) REFERENCES t_m_file(id),
	CONSTRAINT task_detail_question_fk FOREIGN KEY(task_question_id) REFERENCES t_m_task_question(id)
);

CREATE TABLE t_r_submission (
	id SERIAL,
    grade FLOAT,
	teacher_notes TEXT,
    student_id INT NOT NULL,
    task_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT submission_pk PRIMARY KEY(id),
    CONSTRAINT submission_ck UNIQUE(student_id, task_id),
    CONSTRAINT submission_task_fk FOREIGN KEY(task_id) REFERENCES t_m_task(id),
    CONSTRAINT submission_student_fk FOREIGN KEY(task_id) REFERENCES t_m_user(id)
);

CREATE TABLE t_r_submission_detail (
	id SERIAL,
    essay_answer_content TEXT,
	submission_id INT NOT NULL,
    question_id INT NOT NULL,
    student_id INT NOT NULL,
    choice_option_id INT,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT submission_detail_pk PRIMARY KEY(id),
    CONSTRAINT submission_detail_ck UNIQUE(question_id, student_id, choice_option_id),
	CONSTRAINT submission_detail_submission_id_fk FOREIGN KEY(submission_id) REFERENCES t_r_submission(id),
    CONSTRAINT submission_detail_question_fk FOREIGN KEY(question_id) REFERENCES t_m_task_question(id),
    CONSTRAINT submission_detail_student_fk FOREIGN KEY(student_id) REFERENCES t_m_user(id),
    CONSTRAINT submission_detail_option_fk FOREIGN KEY(choice_option_id) REFERENCES t_m_task_multiple_choice_option(id)
);

CREATE TABLE t_r_submission_detail_file (
	id SERIAL,
    submission_id INT NOT NULL,
    student_id INT NOT NULL,
    file_id INT NOT NULL,
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	updated_by INT,
	ver INT NOT NULL,
	is_active BOOLEAN NOT NULL,
    CONSTRAINT submission_file_pk PRIMARY KEY(id),
	CONSTRAINT submission_file_ck UNIQUE(submission_id, student_id, file_id),
    CONSTRAINT submission_id_fk FOREIGN KEY(submission_id) REFERENCES t_r_submission(id),
    CONSTRAINT submission_file_student_fk FOREIGN KEY(student_id) REFERENCES t_m_user(id),
    CONSTRAINT submission_file_fk FOREIGN KEY(file_id) REFERENCES t_m_file(id)
);

INSERT INTO t_m_file (file_content, file_extension, created_by, created_at, ver, is_active) VALUES
	('file_1', 'jpg', 1, NOW(), 1, true),
	('file_2', 'png', 1, NOW(), 1, true),
	('file_3', 'jpeg', 1, NOW(), 1, true),
	('file_4', 'jpg', 1, NOW(), 1, true),
	('file_5', 'jpg', 1, NOW(), 1, true);

INSERT INTO t_m_role (role_code, role_name, created_by, created_at, ver, is_active) VALUES
	('SA', 'Super Admin', 1, NOW(), 1, true),
	('STD', 'Student', 1, NOW(), 1, true),
	('TCH', 'Teacher', 1, NOW(), 1, true);

INSERT INTO t_m_user (full_name, email, pass, photo_id, role_id, created_by, created_at, ver, is_active) VALUES
	('Andi Susilo', 'andi@gmail.com', 'andi', NULL, 1, 1, NOW(), 1, true),
	('Budi Doremi', 'budi@gmail.com', 'budi', NULL, 2, 1, NOW(), 1, true),
	('Caca Putri', 'caca@gmail.com', 'caca', NULL, 2, 1, NOW(), 1, true),
	('Deni Putra', 'deni@gmail.com', 'deni', NULL, 2, 1, NOW(), 1, true),
	('Eka Setiawan', 'eka@gmail.com', 'eka', NULL, 2, 1, NOW(), 1, true);

INSERT INTO t_m_class (class_code, class_title, class_description, class_image_id, created_by, created_at, ver, is_active) VALUES
	('JAVA-1', 'Java basics 1', 'Learn java basics level 1', 1, 1, NOW(), 1, true),
	('JAVA-2', 'Java basics 2', 'Learn java basics level 2', 2, 1, NOW(), 1, true),
	('JAVA-3', 'Java basics 3', 'Learn java basics level 3', 3, 1, NOW(), 1, true),
	('JAVA-4', 'Java basics 4', 'Learn java basics level 4', 4, 1, NOW(), 1, true),
	('JAVA-5', 'Java basics 5', 'Learn java basics level 5', 4, 1, NOW(), 1, true);

INSERT INTO t_r_student_class (class_id, student_id, created_by, created_at, ver, is_active) VALUES
	(1, 2, 1, NOW(), 1, true),
	(1, 3, 1, NOW(), 1, true),
	(2, 2, 1, NOW(), 1, true),
	(2, 4, 1, NOW(), 1, true),
	(3, 4, 1, NOW(), 1, true);

INSERT INTO t_m_learning (learning_name, learning_description, learning_date, class_id, created_by, created_at, ver, is_active) VALUES
	('Learning no 1', 'Learning - Basic consept of SOLID Principle no 1', NOW(), 1, 1, NOW(), 1, true),
	('Learning no 2', 'Learning - Basic consept of SOLID Principle no 2', NOW(), 1, 1, NOW(), 1, true),
	('Learning no 3', 'Learning - Basic consept of SOLID Principle no 3', NOW(), 2, 1, NOW(), 1, true),
	('Learning no 4', 'Learning - Basic consept of SOLID Principle no 4', NOW(), 2, 1, NOW(), 1, true),
	('Learning no 5', 'Learning - Basic consept of SOLID Principle no 5', NOW(), 3, 1, NOW(), 1, true);

INSERT INTO t_m_session (session_name, session_description, start_time, end_time, learning_id, created_by, created_at, ver, is_active) VALUES
	('Session no 1', 'Session - Basic consept of SOLID Principle no 1', NOW(), NOW(), 1, 1, NOW(), 1, true),
	('Session no 2', 'Session - Basic consept of SOLID Principle no 2', NOW(), NOW(), 1, 1, NOW(), 1, true),
	('Session no 3', 'Session - Basic consept of SOLID Principle no 3', NOW(), NOW(), 2, 1, NOW(), 1, true),
	('Session no 4', 'Session - Basic consept of SOLID Principle no 4', NOW(), NOW(), 2, 1, NOW(), 1, true),
	('Session no 5', 'Session - Basic consept of SOLID Principle no 5', NOW(), NOW(), 3, 1, NOW(), 1, true);

INSERT INTO t_m_forum (forum_name, forum_description, session_id, created_by, created_at, ver, is_active) VALUES
	('Forum-1', 'forum description no 1', 1, 1, NOW(), 1, true),
	('Forum-2', 'forum description no 2', 2, 1, NOW(), 1, true),
	('Forum-3', 'forum description no 3', 3, 1, NOW(), 1, true),
	('Forum-4', 'forum description no 4', 4, 1, NOW(), 1, true),
	('Forum-5', 'forum description no 5', 5, 1, NOW(), 1, true);

INSERT INTO t_r_forum_comment (comment_content, user_id, forum_id, created_by, created_at, ver, is_active) VALUES
	('I still could not understand the no 1 principle, please help!', 1, 1, 1, NOW(), 1, true),
	('I still could not understand the no 2 principle, please help!', 1, 1, 1, NOW(), 1, true),
	('I still could not understand the no 3 principle, please help!', 2, 2, 1, NOW(), 1, true),
	('Thankyou for the material, it is exhaustive and easy to understand.', 2, 2, 1, NOW(), 1, true),
	('Thankyou for the material, it is exhaustive and easy to understand.', 2, 2, 1, NOW(), 1, true);

INSERT INTO t_r_session_attendance (is_approved, student_id, session_id, created_by, created_at, ver, is_active) VALUES
	(false, 2, 1, 1, NOW(), 1, true),
	(true, 3, 1, 1, NOW(), 1, true),
	(true, 2, 2, 1, NOW(), 1, true),
	(true, 4, 2, 1, NOW(), 1, true),
	(false, 4, 3, 1, NOW(), 1, true);

INSERT INTO t_m_session_material (material_name, material_description, session_id, created_by, created_at, ver, is_active) VALUES
	('material-1', 'This is module for SOLID Principle no 1', 1, 1, NOW(), 1, true),
	('material-2', 'This is module for SOLID Principle no 2', 1, 1, NOW(), 1, true),
	('material-3', 'This is module for SOLID Principle no 3', 2, 1, NOW(), 1, true),
	('material-4', 'This is module for SOLID Principle no 4', 2, 1, NOW(), 1, true),
	('material-5', 'This is module for SOLID Principle no 5', 3, 1, NOW(), 1, true);

INSERT INTO t_r_session_material_file (file_id, material_id, created_by, created_at, ver, is_active) VALUES
	(1, 1, 1, NOW(), 1, true),
	(2, 1, 1, NOW(), 1, true),
	(3, 2, 1, NOW(), 1, true),
	(4, 2, 1, NOW(), 1, true),
	(5, 3, 1, NOW(), 1, true);

INSERT INTO t_m_task (task_name, task_description, duration, session_id, created_by, created_at, ver, is_active) VALUES
	('Task-1', 'Give implementation example for SOLID Principle no 1', 10, 1, 1, NOW(), 1, true),
	('Task-2', 'Give implementation example for SOLID Principle no 2', 20, 2, 1, NOW(), 1, true),
	('Task-3', 'Give implementation example for SOLID Principle no 3', 30, 3, 1, NOW(), 1, true),
	('Task-4', 'Give implementation example for SOLID Principle no 4', 40, 4, 1, NOW(), 1, true),
	('Task-5', 'Give implementation example for SOLID Principle no 5', 50, 5, 1, NOW(), 1, true);

INSERT INTO t_r_task_file (file_name, file_id, created_by, created_at, ver, is_active) VALUES
	('Explanation for task no 1', 2, 1, NOW(), 1, true),
	('Explanation for task no 2', 2, 1, NOW(), 1, true),
	('Explanation for task no 3', 3, 1, NOW(), 1, true),
	('Explanation for task no 4', 3, 1, NOW(), 1, true),
	('Explanation for task no 5', 4, 1, NOW(), 1, true);

INSERT INTO t_m_task_question (question_type, question_content, created_by, created_at, ver, is_active) VALUES
	('Essay', 'Explain SOLID Principle no 1', 1, NOW(), 1, true),
	('Essay', 'Explain SOLID Principle no 2', 1, NOW(), 1, true),
	('Multiple Choice', 'What is SOLID Principle no 2', 1, NOW(), 1, true),
	('Multiple Choice', 'What is SOLID Principle no 3', 1, NOW(), 1, true),
	('Multiple Choice', 'Explain SOLID Principle no 4', 1, NOW(), 1, true);

INSERT INTO t_m_task_multiple_choice_option (option_char, option_text, is_correct, question_id, created_by, created_at, ver, is_active) VALUES
	('A', 'Single responsibility', true, 1, 1, NOW(), 1, true),
	('B', 'Open-closed', true, 1, 1, NOW(), 1, true),
	('C', 'Liskov substitution', true, 2, 1, NOW(), 1, true),
	('D', 'Interface seggregation', true, 2, 1, NOW(), 1, true),
	('E', 'Dependency inversion', true, 3, 1, NOW(), 1, true);

INSERT INTO t_r_task_detail (task_id, task_file_id, task_question_id, created_by, created_at, ver, is_active) VALUES
	(1, NULL, 1, 1, NOW(), 1, true),
	(1, NULL, 1, 1, NOW(), 1, true),
	(2, NULL, 2, 1, NOW(), 1, true),
	(2, NULL, 2, 1, NOW(), 1, true),
	(3, NULL, 3, 1, NOW(), 1, true),
	(3, 1, NULL, 1, NOW(), 1, true),
	(4, 1, NULL, 1, NOW(), 1, true),
	(5, 2, NULL, 1, NOW(), 1, true),
	(5, 3, NULL, 1, NOW(), 1, true);

-- -- Tidy up below before working on teacher's features
-- INSERT INTO t_r_submission (grade, teacher_notes, student_id, task_id, created_by, created_at, ver, is_active) VALUES
-- 	(NULL, NULL, 1, 1, 1, NOW(), 1, true),
-- 	(60, 'Good Enough', 2, 1, 1, NOW(), 1, true),
-- 	(70, 'Nice try', 1, 2, 1, NOW(), 1, true),
-- 	(80, 'Nice try', 2, 2, 1, NOW(), 1, true),
-- 	(90, 'Nice try', 1, 3, 1, NOW(), 1, true);

-- INSERT INTO t_r_submission_detail (essay_answer_content, submission_id, question_id, student_id, choice_option_id, created_by, created_at, ver, is_active) VALUES
-- 	('Liskov substitution', 1, 1, 1, NULL, 1, NOW(), 1, true),
-- 	('Interface seggregation', 2, 2, 2, NULL, 1, NOW(), 1, true),
-- 	('Dependency inversion', 3, 3, 3, NULL, 1, NOW(), 1, true),
-- 	(NULL, 4, 4, 4, 4, 1, NOW(), 1, true),
-- 	(NULL, 5, 5, 5, 5, 1, NOW(), 1, true);

-- INSERT INTO t_r_submission_detail_file (submission_id, student_id, file_id, created_by, created_at, ver, is_active) VALUES
-- 	(1, 1, 1, 1, NOW(), 1, true),
-- 	(2, 2, 2, 1, NOW(), 1, true),
-- 	(3, 3, 3, 1, NOW(), 1, true),
-- 	(4, 4, 4, 1, NOW(), 1, true),
-- 	(5, 5, 5, 1, NOW(), 1, true);
