-- get class list by student
SELECT
	c.id,
	c.class_code,
	c.class_title,
	c.class_description,
	t.full_name,
	f.file_content,
	f.file_extension,
	l.learning_name,
	l.learning_description,
	l.learning_date,
	s.id AS session_id,
	s.session_name,
	s.session_description,
	s.start_time,
	s.end_time
FROM
	t_m_class c
INNER JOIN
	t_m_user t ON t.id = c.teacher_id
INNER JOIN
	t_m_file f ON c.class_image_id = f.id
INNER JOIN
	t_r_student_class sc ON c.id = sc.class_id
INNER JOIN
	t_m_learning l ON c.id = l.class_id
INNER JOIN
	t_m_session s ON l.id = s.learning_id
WHERE
	sc.student_id = 2

-- get attendance status
SELECT 
	is_approved
FROM 
	t_r_session_attendance
WHERE
	student_id = 2
	AND session_id = 1

-- get session by id
SELECT
	s.id,
	s.session_name,
	s.session_description,
	s.start_time,
	s.end_time,
	f.id AS forum_id,
	f.forum_name,
	m.id AS material_id,
	m.material_name,
	m.material_description,
	mf.id AS mf_id,
	mf.file_name,
	mff.file_content,
	mff.file_extension,
	t.id AS task_id,
	t.task_name,
	t.task_description,
	t.duration,
	tf.file_name,
	tff.file_content,
	tff.file_extension,
	q.question_type,
	q.question_content,
	mco.option_char,
	mco.option_text,
	mco.is_correct
FROM
	t_m_session s
LEFT JOIN
	t_m_forum f ON s.id = f.session_id
LEFT JOIN
	t_m_session_material m ON s.id = m.session_id
LEFT JOIN
	t_r_session_material_file mf ON m.id = mf.material_id
LEFT JOIN
	t_m_file mff ON mf.file_id = mff.id
LEFT JOIN
	t_m_task t ON s.id = t.session_id
LEFT JOIN
	t_r_task_file tf ON t.id = tf.task_id
LEFT JOIN
	t_m_file tff ON tf.file_id = tff.id
LEFT JOIN
	t_m_task_question q ON t.id = q.task_id
LEFT JOIN
	t_m_task_multiple_choice_option mco ON q.id = mco.question_id
WHERE
	s.id = 2;

-- get material list by session id
SELECT
	m.id,
	m.material_name,
	m.material_description,
	mf.id AS mf_id,
	mf.file_name,
	mff.file_content,
	mff.file_extension
FROM
	t_m_session_material m
LEFT JOIN
	t_r_session_material_file mf ON m.id = mf.material_id
LEFT JOIN
	t_m_file mff ON mf.file_id = mff.id
WHERE
	m.session_id = 2;

-- get task list by session id
SELECT
    t.id,
    t.task_name,
    t.task_description,
    t.duration,
    tf.file_name,
    tff.file_content,
    tff.file_extension,
    q.id AS question_id,
    q.question_type,
    q.question_content,
    mco.option_char,
    mco.option_text,
    mco.is_correct
FROM
    t_m_task t
LEFT JOIN
    t_r_task_detail td ON t.id = td.task_id 
LEFT JOIN
    t_r_task_file tf ON td.task_file_id = tf.id
LEFT JOIN
    t_m_file tff ON tf.file_id = tff.id
LEFT JOIN
    t_m_task_question q ON td.task_question_id = q.id
LEFT JOIN
    t_m_task_multiple_choice_option mco ON q.id = mco.question_id
WHERE
    t.session_id = 2;
