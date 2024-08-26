  --ユーザーの作成
--ユーザーを切り替え
\c postgres


-- Project Name : Todo
-- Date/Time    : 2024/08/24 14:48:42
-- Author       : es_win11_user
-- RDBMS Type   : PostgreSQL
-- Application  : A5:SQL Mk-2

/*
  << 注意！！ >>
  BackupToTempTable, RestoreFromTempTable疑似命令が付加されています。
  これにより、drop table, create table 後もデータが残ります。
  この機能は一時的に $$TableName のような一時テーブルを作成します。
  この機能は A5:SQL Mk-2でのみ有効であることに注意してください。
*/

-- User
-- * RestoreFromTempTable
create table user_info (
  user_id varchar(100)
  , user_name varchar(100) not null
  , email varchar(100) not null
  , is_started boolean not null
  , constraint user_info_PKC primary key (user_id)
) ;

-- TodoItem
-- * RestoreFromTempTable
create table todo_item (
  todo_item_id varchar(100)
  , todo_id varchar(100) not null
  , title varchar(100)
  , schedule_start_date timestamp not null
  , schedule_end_date timestamp not null
  , start_date timestamp
  , end_date timestamp
  , amount decimal(10,0) not null
  , constraint todo_item_PKC primary key (todo_item_id)
) ;

-- Todo
-- * RestoreFromTempTable
create table todo (
  todo_id varchar(100)
  , user_id varchar(100) not null
  , title varchar(100) not null
  , description text
  , schedule_start_date timestamp not null
  , schedule_end_date timestamp not null
  , constraint todo_PKC primary key (todo_id)
) ;

comment on table user_info is 'User';
comment on column user_info.user_id is 'UserId';
comment on column user_info.user_name is 'UserName';
comment on column user_info.email is 'EMail';
comment on column user_info.is_started is 'IsStarted';

comment on table todo_item is 'TodoItem';
comment on column todo_item.todo_item_id is 'TodoItemId';
comment on column todo_item.todo_id is 'TodoId';
comment on column todo_item.title is 'Title';
comment on column todo_item.schedule_start_date is 'ScheduleStartDate';
comment on column todo_item.schedule_end_date is 'ScheduleEndDate';
comment on column todo_item.start_date is 'StartDate';
comment on column todo_item.end_date is 'EndDate';
comment on column todo_item.amount is 'Amount';

comment on table todo is 'Todo';
comment on column todo.todo_id is 'TodoId';
comment on column todo.user_id is 'UserId';
comment on column todo.title is 'Title';
comment on column todo.description is 'Description';
comment on column todo.schedule_start_date is 'ScheduleStartDate';
comment on column todo.schedule_end_date is 'ScheduleEndDate';







insert into public.todo(todo_id,user_id,title,description,schedule_start_date,schedule_end_date) values 
    ('TODO01','USER01','タイトル3','詳細',TIMESTAMP '2024-08-01 00:00:00.000',TIMESTAMP '2024-08-02 00:00:00.000');



insert into public.todo_item(todo_item_id,todo_id,title,schedule_start_date,schedule_end_date,start_date,end_date,amount) values 
    ('TODOITEM01','TODO01','アイテムタイトル1',TIMESTAMP '2024-08-01 10:00:00.000',TIMESTAMP '2024-08-02 17:31:52.264',TIMESTAMP '2024-08-01 17:32:00.285',null,0);
