-- freshbox_create_tables_dev.sql
-- 테이블 스키마 정의 및 생성 스크립트
-- 작성자: 최마리, 서예경
-- 최종 편집일 : 2025-06-07
-- 개발용: ON DELETE CASCADE 포함

CREATE DATABASE FreshBox CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;

/* 
CREATE DATABASE FreshBox 
	→ “FreshBox”라는 새 데이터베이스를 만드세요 라는 명령어.
CHARACTER SET utf8mb4
	→ 이 DB에서 사용할 문자 인코딩(글자 모양)을 utf8mb4로 설정.
	→ UTF-8의 확장 버전으로, 이모지(😀) 같은 특수문자도 다 지원함
COLLATE utf8mb4_general_ci
	→ 대소문자 구분 없이 비교하는 설정(예 - ‘a’와 ‘A’를 같은 문자로 취급)
*/

-- FreshBox 데이터베이스를 사용하겠다고 지정하는 명령어
-- 이걸 실행해야 이후의 쿼리(테이블 생성, 데이터 삽입 등)가 FreshBox 기준으로 작동
USE FreshBox;

-- 현재 선택된 데이터베이스 이름을 확인하는 명령어
-- 만약 위에서 USE FreshBox;가 제대로 적용됐다면, 여기 결과로 'FreshBox'가 출력돼야 함!
SELECT DATABASE();

-- MEMBER 테이블 (사용자)
CREATE TABLE member (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    member_name VARCHAR(30) NOT NULL,
    role ENUM('employee', 'admin') NOT NULL,
    phone VARCHAR(20) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    birth_date DATE NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    hire_date DATE
) ENGINE=InnoDB;

-- CATEGORY 테이블
CREATE TABLE category (
    id INT AUTO_INCREMENT PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE
) ENGINE=InnoDB;

-- WAREHOUSE 테이블
CREATE TABLE warehouse (
    id INT AUTO_INCREMENT PRIMARY KEY,
    location VARCHAR(255),
    temp_control ENUM('frozen', 'refrigerated', 'room') NOT NULL
) ENGINE=InnoDB;

CREATE TABLE product (
    id INT AUTO_INCREMENT PRIMARY KEY COMMENT '상품 ID',
    product_name VARCHAR(100) NOT NULL COMMENT '상품명',
    stock INT COMMENT '재고량',
    target_stock INT COMMENT '목표 재고량',
    barcode VARCHAR(100) NOT NULL UNIQUE COMMENT '바코드 번호',
    storage_temp ENUM('frozen', 'refrigerated', 'room') NOT NULL COMMENT '보관 온도 구분',
    warehouse_id INT NOT NULL COMMENT '보관 창고 ID',
    category_id INT NOT NULL COMMENT '카테고리 ID',

    FOREIGN KEY (warehouse_id) REFERENCES warehouse(id),
    FOREIGN KEY (category_id) REFERENCES category(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='상품 정보';

-- WORK_LOG 테이블
CREATE TABLE work_log (
    id INT AUTO_INCREMENT PRIMARY KEY COMMENT '출퇴근 로그 ID',
    member_id INT NOT NULL COMMENT '사용자 ID',
    check_in DATETIME COMMENT '출근 시간',
    check_out DATETIME COMMENT '퇴근 시간',
    work_date DATE NOT NULL COMMENT '출퇴근 기준 날짜',
    work_duration INT COMMENT '근무 시간(분 단위)',
    status ENUM('Normal', 'Late', 'EarlyLeave', 'Absent', 'Vacation') COMMENT '출근 상태(정상, 늦음, 조기 퇴근, 결근, 휴가)',
    recorded_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '해당 로그가 기록된 시간 (감사 및 로깅용)',
    modified_at DATETIME COMMENT '수동 수정 시 시간 기록 (HR 관리자용 변경 추적)',
    is_manual TINYINT DEFAULT 0 COMMENT '사람이 수동으로 수정했는지 여부 (1=수동, 0=자동)',
    work_setting_id INT NOT NULL COMMENT '기준이 된 출퇴근 시간 설정 ID',
    FOREIGN KEY (member_id) REFERENCES member(id)
	    ON DELETE CASCADE,
	  FOREIGN KEY (work_setting_id) REFERENCES work_time_settings(id)
		  ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='출퇴근 기록 로그';

-- MY_ORDER 테이블
CREATE TABLE my_order (
    id INT AUTO_INCREMENT PRIMARY KEY,
    order_date DATETIME,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- INBOUND_LOG 테이블
CREATE TABLE inbound_log (
    id INT AUTO_INCREMENT PRIMARY KEY,     -- 입고 기록 ID (PK, Auto Increment)
    inbound_at DATETIME DEFAULT CURRENT_TIMESTAMP, -- 입고 시간, 기본값 현재시간
    order_id INT NOT NULL, -- 발주 내용 확인용 (FK -> MY_ORDER.id)
    product_id INT NOT NULL, -- 상품 ID (FK -> PRODUCT.id)
    quantity INT NOT NULL, -- 입고 수량
    member_id INT NOT NULL, -- 처리 사원 ID (FK -> MEMBER.id)
    FOREIGN KEY (order_id) REFERENCES my_order(id)
        ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES product(id)
        ON DELETE CASCADE,
    FOREIGN KEY (member_id) REFERENCES member(id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- EXPIRATION_DATE 테이블
CREATE TABLE expiration_date (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    inbound_log_id INT NOT NULL,
    expiry_date DATE NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(id)
        ON DELETE CASCADE,
    FOREIGN KEY (inbound_log_id) REFERENCES inbound_log(id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- OUTBOUND_REQUEST 테이블
CREATE TABLE outbound_request (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    target_address VARCHAR(100) NOT NULL,
    FOREIGN KEY (product_id) REFERENCES PRODUCT(id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- OUTBOUND_LOG 테이블
CREATE TABLE outbound_log (
    id INT AUTO_INCREMENT PRIMARY KEY,
    outbound_rq_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    outbound_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    member_id INT NOT NULL,
    FOREIGN KEY (outbound_rq_id) REFERENCES outbound_request(id),
    FOREIGN KEY (product_id) REFERENCES product(id) 
        ON DELETE CASCADE,
    FOREIGN KEY (member_id) REFERENCES member(id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- WAREHOUSE_TEMPERATURE_LOG 테이블
CREATE TABLE warehouse_temperature_log (
    id INT AUTO_INCREMENT PRIMARY KEY,
    warehouse_id INT NOT NULL,
    temperature DECIMAL(4,1) NOT NULL,
    measured_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (warehouse_id) REFERENCES warehouse(id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- ENGINE=InnoDB;는 MySQL에서 테이블을 만들 때 사용하는 스토리지 엔진을 지정
-- 이 테이블을 InnoDB(이노디비) 방식으로 만든다"는 뜻
-- MySQL은 엔진 종류가 많다
-- 외래키(FK)를 쓰려면 이노디비 엔진을 꼭 써야함
-- 다른 엔진은 외래키 제약조건 지원하지 않아서 FK로 선언해도 무시된다고함
-- 외래키 무결성 보장, ON DELETE CASCADE 같은 옵션도 InnoDB에서만 작동
-- 즉, FK 쓰려면 ENGINE=InnoDB는 필수!

-- 모든 PK는 불변(Immutable) 값이므로, ON UPDATE CASCADE는 생략(기본 동작: RESTRICT)

-- 추가 테이블 

-- 근무 가능 시간 설정 테이블
CREATE TABLE work_time_settings (
    id INT AUTO_INCREMENT PRIMARY KEY COMMENT '근무 시간 설정 ID',
    work_start_from TIME NOT NULL COMMENT '출근 가능 시작 시간 (예: 06:00:00)',
    work_start_to TIME NOT NULL COMMENT '출근 마감 시간 (예: 08:00:00)',
    work_end_from TIME NOT NULL COMMENT '퇴근 시작 시간 (예: 17:00:00)',
    work_end_to TIME NOT NULL COMMENT '퇴근 마감 시간 (예: 23:59:59)',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '생성 시각',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '수정 시각 자동 갱신'
) ENGINE=InnoDB COMMENT='근무 가능 시간 범위 설정 테이블';

-- 출퇴근 로그 수정 요청 테이블
CREATE TABLE work_log_edit_requests (
    id INT AUTO_INCREMENT PRIMARY KEY COMMENT '수정 요청 ID',
    work_log_id INT NOT NULL COMMENT '수정 대상 출퇴근 로그 ID',
    member_id INT NOT NULL COMMENT '요청자 사원 ID',
    requested_check_in DATETIME COMMENT '요청 출근 시간 (없으면 NULL)',
    requested_check_out DATETIME COMMENT '요청 퇴근 시간 (없으면 NULL)',
    reason VARCHAR(1000) NOT NULL COMMENT '수정 요청 사유',
    status ENUM('Pending', 'Approved', 'Rejected') NOT NULL DEFAULT 'Pending' COMMENT '요청 상태',
    request_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '요청 일시',
    decision_date DATETIME COMMENT '승인/거절 일시',
    admin_id INT COMMENT '요청 처리 관리자 ID',
    comment VARCHAR(1000) COMMENT '관리자 코멘트',
 CONSTRAINT fk_work_log_id FOREIGN KEY (work_log_id) REFERENCES work_log(id)
	     ON DELETE CASCADE,
    CONSTRAINT fk_member_id FOREIGN KEY (member_id) REFERENCES member(id)
	     ON DELETE CASCADE,
    CONSTRAINT fk_admin_id FOREIGN KEY (admin_id) REFERENCES member(id)
	     ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='출퇴근 로그 수정 요청 테이블';

-- 회사 고유 휴일 테이블 (법정 공휴일 제외)
CREATE TABLE company_holidays (
    id INT AUTO_INCREMENT PRIMARY KEY COMMENT '회사 고유 휴일 ID',
    date DATE NOT NULL UNIQUE COMMENT '휴일 날짜',
    reason VARCHAR(500) NOT NULL COMMENT '휴일 이유'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='회사 전용 휴일 테이블';

-- 출퇴근 체크용 허용 와이파이 목록 테이블
CREATE TABLE allowed_wifi (
    id INT AUTO_INCREMENT PRIMARY KEY COMMENT '허용 와이파이 ID',
    ssid VARCHAR(100) NOT NULL COMMENT '와이파이 SSID',
    mac_addr CHAR(17) NOT NULL UNIQUE COMMENT 'MAC 주소 (17자 고정)',
    location_name VARCHAR(100) COMMENT '설치 장소 이름',
    is_active TINYINT DEFAULT 1 COMMENT '활성화 여부 (1=활성, 0=비활성)',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '등록 시각'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='출퇴근 허용 와이파이 목록';



/*

 DB설계 기여도

### 최마리 
- DB 구조 설계 및 초기 스키마 작성 (테이블 10개, 불량품 기록 테이블 제거)
- 테이블 간 관계 설정 (PK/FK) 
- 테이블 생성 전체 SQL문 작성 및 정규화
- ERD 설계 및 데이터 흐름 정의
- 추가 설계: work_time_settings ,work_log_edit_requests, 
	company_holidays, allowed_wifi 테이블 설계 및 SQL 작성

### 서예경 
- 테이블 2개 추가 설계(my_order, outbound_request), 추가 테이블 간 관계 설정 (PK/FK)
- 기존 테이블 내 일부 컬럼 정제 : product(6개), warehouse(2개) 
- product테이블 생성 SQL에 COMMENT문 추가
- 테이블용 샘플/더미 데이터 INSERT문 작성 및 삽입 
- 본인 작업 내용에 따른 테이블 생성 SQL문 일부 수정 및 반영

*/