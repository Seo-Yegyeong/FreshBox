-- freshbox_create_tables_prod.sql
-- 테이블 스키마 정의 및 생성 스크립트
-- 작성자: 최마리, 서예경
-- 최종 편집일 : 2025-06-07
-- CASCADE 제거된 실운영 버전

-- 운영 환경에서는 물리 삭제 대신 
-- 소프트 삭제(is_deleted) 또는 상태(status) 변경으로 관리(권장)
/* 소프트 삭제 : is_deleted 컬럼을 추가하고, 
	 삭제 시 해당 컬럼을 TRUE로 변경하여 레코드를 비활성화 */
/* 상태 변경: status 컬럼을 추가하여 'active', 'inactive' 등 
	 상태로 구분하며 물리 삭제 대신 상태로 관리*/
	 
-- 예시:
--   DELETE 대신: UPDATE MEMBER SET is_deleted = 1 WHERE id = ?;
--   조회 시: SELECT * FROM MEMBER WHERE is_deleted = 0;
--   상품 단종 처리: UPDATE PRODUCT SET status = 'discontinued' WHERE id = ?;

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
    id INT AUTO_INCREMENT PRIMARY KEY COMMENT '로그 ID',
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
    FOREIGN KEY (member_id) REFERENCES member(id),
    FOREIGN KEY (work_setting_id) REFERENCES work_time_settings(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='출퇴근 기록 로그';


-- MY_ORDER 테이블
CREATE TABLE my_order (
    id INT AUTO_INCREMENT PRIMARY KEY,
    order_date DATETIME,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(id)
) ENGINE=InnoDB;

-- INBOUND_LOG 테이블
CREATE TABLE inbound_log (
    id INT AUTO_INCREMENT PRIMARY KEY,     -- 입고 기록 ID (PK, Auto Increment)
    inbound_at DATETIME DEFAULT CURRENT_TIMESTAMP, -- 입고 시간, 기본값 현재시간
    order_id INT NOT NULL, -- 발주 내용 확인용 (FK -> MY_ORDER.id)
    product_id INT NOT NULL, -- 상품 ID (FK -> PRODUCT.id)
    quantity INT NOT NULL, -- 입고 수량
    member_id INT NOT NULL, -- 처리 사원 ID (FK -> MEMBER.id)
    FOREIGN KEY (order_id) REFERENCES my_order(id),
    FOREIGN KEY (product_id) REFERENCES product(id),
    FOREIGN KEY (member_id) REFERENCES member(id)
) ENGINE=InnoDB;

-- EXPIRATION_DATE 테이블
CREATE TABLE expiration_date (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    inbound_log_id INT NOT NULL,
    expiry_date DATE NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(id),
    FOREIGN KEY (inbound_log_id) REFERENCES inbound_log(id)
) ENGINE=InnoDB;

-- OUTBOUND_REQUEST 테이블
CREATE TABLE outbound_request (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    target_address VARCHAR(100) NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(id)
) ENGINE=InnoDB;

-- OUTBOUND_LOG 테이블
CREATE TABLE outbound_log (
    id INT AUTO_INCREMENT PRIMARY KEY,
    outbound_rq_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    outbound_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    member_id INT NOT NULL,
    FOREIGN KEY (outbound_rq_id) REFERENCES outbound_request(id),
    FOREIGN KEY (product_id) REFERENCES product(id),
    FOREIGN KEY (member_id) REFERENCES member(id)
) ENGINE=InnoDB;

-- WAREHOUSE_TEMPERATURE_LOG 테이블
CREATE TABLE warehouse_temperature_log (
    id INT AUTO_INCREMENT PRIMARY KEY,
    warehouse_id INT NOT NULL,
    temperature DECIMAL(4,1) NOT NULL,
    measured_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (warehouse_id) REFERENCES warehouse(id)
) ENGINE=InnoDB;

-- ENGINE=InnoDB;는 MySQL에서 테이블을 만들 때 사용하는 스토리지 엔진을 지정
-- 이 테이블을 InnoDB(이노디비) 방식으로 만든다"는 뜻
-- MySQL은 엔진 종류가 많다
-- 외래키(FK)를 쓰려면 이노디비 엔진을 꼭 써야함
-- 다른 엔진은 외래키 제약조건 지원하지 않아서 FK로 선언해도 무시된다고함
-- 외래키 무결성 보장, ON DELETE CASCADE 같은 옵션도 InnoDB에서만 작동
-- 즉, FK 쓰려면 ENGINE=InnoDB는 필수!

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
    CONSTRAINT fk_work_log_id FOREIGN KEY (work_log_id) REFERENCES work_log(id),
    CONSTRAINT fk_member_id FOREIGN KEY (member_id) REFERENCES member(id),
    CONSTRAINT fk_admin_id FOREIGN KEY (admin_id) REFERENCES member(id)
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