-- freshbox_drop_tables.sql  
-- 💣 FreshBox 프로젝트의 모든 테이블을 삭제하는 스크립트입니다.
-- ⚠️ 실행 시 기존 데이터가 전부 사라지니 주의하세요!

-- 먼저 외래키 제약조건을 잠깐 꺼줍니다 (순서 상관 없이 테이블 삭제하려면 필요)
SET FOREIGN_KEY_CHECKS = 0;

-- 테이블 삭제 (존재할 경우에만 삭제되도록 IF EXISTS 추가)
DROP TABLE IF EXISTS work_log_edit_requests;
DROP TABLE IF EXISTS work_time_settings;
DROP TABLE IF EXISTS warehouse_temperature_log;
DROP TABLE IF EXISTS outbound_log;
DROP TABLE IF EXISTS outbound_request;
DROP TABLE IF EXISTS expiration_date;
DROP TABLE IF EXISTS inbound_log;
DROP TABLE IF EXISTS my_order;
DROP TABLE IF EXISTS work_log;
DROP TABLE IF EXISTS product;
DROP TABLE IF EXISTS warehouse;
DROP TABLE IF EXISTS category;
DROP TABLE IF EXISTS member;

-- 외래키 제약조건 다시 켜기
SET FOREIGN_KEY_CHECKS = 1;

-- ✔️ 모든 테이블 삭제 완료
-- ⚠️ DATABASE 자체는 삭제되지 않음. 필요시 DROP DATABASE FreshBox; 추가 가능