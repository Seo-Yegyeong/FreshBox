-- freshbox_insert_sample_data.sql
-- 테이블용 샘플/더미 데이터 INSERT문 작성 및 삽입 
-- 작성자: 서예경
-- 작성일: 2025-05-28


-- Inserts for CATEGORY
INSERT INTO CATEGORY (id, category_name) VALUES (1, '과자');
INSERT INTO CATEGORY (id, category_name) VALUES (2, '쌀/잡곡');
INSERT INTO CATEGORY (id, category_name) VALUES (3, '채소');
INSERT INTO CATEGORY (id, category_name) VALUES (4, '견과');
INSERT INTO CATEGORY (id, category_name) VALUES (5, '수산물/건어물');
INSERT INTO CATEGORY (id, category_name) VALUES (6, '정육/계란');
INSERT INTO CATEGORY (id, category_name) VALUES (7, '델리/치킨/초밥');
INSERT INTO CATEGORY (id, category_name) VALUES (8, '우유/유제품');
INSERT INTO CATEGORY (id, category_name) VALUES (9, '두부/김치/반찬');
INSERT INTO CATEGORY (id, category_name) VALUES (10, '커피/차');

-- Inserts for WAREHOUSE
INSERT INTO WAREHOUSE (id, location, temp_control) VALUES (1, 'A구역 냉장', 'refrigerated');
INSERT INTO WAREHOUSE (id, location, temp_control) VALUES (2, 'B구역 냉장', 'refrigerated');
INSERT INTO WAREHOUSE (id, location, temp_control) VALUES (3, 'A구역 냉동', 'frozen');
INSERT INTO WAREHOUSE (id, location, temp_control) VALUES (4, 'C구역 냉동', 'frozen');
INSERT INTO WAREHOUSE (id, location, temp_control) VALUES (5, 'B구역 실온', 'room');

-- Inserts for MEMBER
-- 비밀번호 암호화(bcrypt 해시)로 인하여 샘플데이터 불가
-- (비밀번호 컬럼에 임의로 평문 넣을시 예외 발생)

-- Inserts for PRODUCT
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (1, '두부', 626, 1005, '4629264802227', 'refrigerated', 2, 9);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (2, '피자', 720, 1391, '0808625911820', 'frozen', 3, 7);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (3, '만두', 227, 744, '3454487278204', 'frozen', 4, 6);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (4, '베이컨', 93, 582, '9280513793786', 'refrigerated', 1, 6);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (5, '즉석밥', 111, 808, '5278081850110', 'room', 5, 2);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (6, '3분카레', 560, 840, '9597568512508', 'room', 5, 5);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (7, '치즈', 560, 1101, '2059306046098', 'refrigerated', 2, 8);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (8, '요거트', 610, 1317, '5160759944972', 'refrigerated', 1, 8);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (9, '냉동볶음밥', 247, 797, '2074361511053', 'frozen', 3, 5);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (10, '소면', 33, 1127, '2690578771082', 'room', 5, 2);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (11, '오예스', 70, 591, '4081155170288', 'room', 5, 1);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (12, '닭강정', 945, 539, '4006528222234', 'frozen', 4, 6);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (13, '컵밥', 857, 1222, '5076033452357', 'room', 5, 2);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (14, '감자튀김', 831, 741, '2675926902905', 'frozen', 3, 7);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (15, '계란', 593, 781, '6220058000195', 'refrigerated', 2, 6);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (16, '오렌지주스', 84, 832, '2260669532580', 'room', 5, 10);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (17, '멸치볶음', 601, 1145, '8612695166378', 'refrigerated', 2, 9);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (18, '샐러드', 16, 1249, '3637691448582', 'refrigerated', 1, 3);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (19, '떡갈비', 380, 1313, '9973409561803', 'frozen', 3, 6);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (20, '돈까스', 835, 563, '6066617972257', 'frozen', 4, 6);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (21, '코코볼', 46, 1336, '6294815525788', 'room', 5, 1);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (22, '떡볶이', 696, 575, '3429546048609', 'frozen', 4, 5);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (23, '포카칩', 620, 1350, '6463522195205', 'room', 5, 1);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (24, '해산물믹스', 93, 879, '9671168822233', 'frozen', 3, 4);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (25, '고등어', 22, 699, '3082759451644', 'frozen', 3, 4);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (26, '붕어싸만코', 215, 1244, '5858268436568', 'frozen', 4, 1);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (27, '누가바', 435, 1135, '8466122099810', 'frozen', 3, 1);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (28, '김자반', 226, 573, '3998149261000', 'room', 5, 5);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (29, '미역국', 446, 684, '5751759473281', 'refrigerated', 2, 9);
INSERT INTO PRODUCT (id, product_name, stock, target_stock, barcode, storage_temp, warehouse_id, category_id) VALUES (30, '참치', 40, 1110, '1820286937642', 'room', 5, 5);

-- Inserts for MY_ORDER
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (1, '2025-01-05T02:39:18', 9, 46);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (2, '2025-04-25T01:39:38', 29, 94);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (3, '2025-01-27T01:43:48', 16, 73);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (4, '2025-03-17T09:13:37', 6, 100);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (5, '2025-02-03T06:28:44', 22, 27);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (6, '2025-03-27T15:02:06', 25, 8);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (7, '2025-04-20T16:49:18', 26, 87);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (8, '2025-01-11T10:02:26', 6, 21);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (9, '2025-03-14T19:51:03', 11, 68);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (10, '2025-01-17T20:29:38', 9, 16);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (11, '2025-05-06T01:31:26', 20, 57);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (12, '2025-04-25T05:16:57', 22, 23);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (13, '2025-03-20T16:18:51', 1, 61);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (14, '2025-03-06T23:00:49', 22, 53);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (15, '2025-02-15T06:18:14', 29, 73);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (16, '2025-04-09T09:50:11', 28, 66);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (17, '2025-03-30T10:53:58', 30, 40);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (18, '2025-01-08T14:14:50', 21, 46);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (19, '2025-04-04T04:41:51', 13, 85);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (20, '2025-05-01T09:25:23', 9, 20);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (21, '2025-01-20T18:26:58', 18, 89);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (22, '2025-02-22T04:11:41', 27, 59);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (23, '2025-04-09T13:07:20', 24, 11);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (24, '2025-04-06T04:52:12', 11, 95);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (25, '2025-04-17T22:52:39', 2, 70);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (26, '2025-05-06T05:51:35', 9, 18);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (27, '2025-03-18T12:35:46', 8, 98);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (28, '2025-04-02T18:50:33', 16, 46);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (29, '2025-02-21T12:25:12', 20, 37);
INSERT INTO MY_ORDER (id, order_date, product_id, quantity) VALUES (30, '2025-02-05T03:36:24', 22, 46);


-- Inserts for EXPIRATION_DATE
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (1, 7, 28, '2025-12-29');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (2, 10, 9, '2025-08-12');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (3, 23, 6, '2025-05-29');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (4, 4, 16, '2025-06-08');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (5, 28, 30, '2026-04-26');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (6, 13, 21, '2025-10-31');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (7, 3, 1, '2025-12-29');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (8, 9, 30, '2026-02-22');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (9, 15, 26, '2026-03-16');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (10, 26, 4, '2026-01-15');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (11, 28, 9, '2025-12-03');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (12, 5, 21, '2026-02-28');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (13, 17, 27, '2026-02-19');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (14, 21, 21, '2025-07-07');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (15, 12, 4, '2026-05-23');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (16, 28, 5, '2025-11-26');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (17, 9, 28, '2025-12-30');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (18, 1, 2, '2025-10-08');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (19, 2, 7, '2026-05-01');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (20, 22, 9, '2026-03-25');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (21, 18, 11, '2025-10-07');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (22, 12, 19, '2026-04-30');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (23, 30, 28, '2025-07-03');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (24, 2, 28, '2026-02-17');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (25, 24, 23, '2025-05-28');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (26, 20, 21, '2025-08-28');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (27, 16, 23, '2025-08-23');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (28, 21, 29, '2025-12-06');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (29, 15, 21, '2026-04-17');
INSERT INTO EXPIRATION_DATE (id, product_id, inbound_log_id, expiry_date) VALUES (30, 14, 12, '2025-09-24');

-- Inserts for OUTBOUND_REQUEST
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (1, 28, 69, '인천광역시 종로구 영동대511거리 (영식이이읍)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (2, 6, 27, '전라북도 고양시 오금46길');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (3, 13, 76, '울산광역시 은평구 언주556거리');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (4, 10, 2, '울산광역시 강동구 봉은사41가 (정호김읍)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (5, 5, 20, '세종특별자치시 성북구 오금가 (경숙박심마을)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (6, 9, 43, '경상북도 화천군 서초중앙5로');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (7, 11, 48, '세종특별자치시 노원구 언주09로');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (8, 23, 12, '강원도 시흥시 삼성길 (성훈한동)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (9, 11, 100, '경기도 수원시 잠실2로');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (10, 20, 5, '강원도 증평군 논현8가');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (11, 2, 35, '대구광역시 성북구 선릉가');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (12, 6, 20, '경기도 태백시 개포로 (도윤이이면)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (13, 19, 38, '충청북도 계룡시 강남대로 (예준한마을)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (14, 12, 51, '경상북도 고양시 덕양구 논현88로');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (15, 18, 17, '세종특별자치시 성동구 서초중앙7거리');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (16, 10, 15, '경상북도 오산시 학동853가');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (17, 16, 94, '전라남도 평창군 압구정08가');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (18, 8, 7, '충청남도 동해시 강남대가 (성수박이리)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (19, 10, 23, '제주특별자치도 청주시 흥덕구 논현거리');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (20, 28, 67, '서울특별시 광진구 도산대92가');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (21, 24, 10, '충청남도 원주시 논현6로 (지우장박면)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (22, 10, 52, '제주특별자치도 천안시 동남구 테헤란979길');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (23, 27, 43, '경기도 남양주시 백제고분83로 (민수김마을)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (24, 10, 54, '강원도 괴산군 가락457길');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (25, 4, 13, '전라남도 공주시 반포대75가');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (26, 18, 62, '대구광역시 성북구 테헤란가');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (27, 16, 44, '제주특별자치도 고양시 덕양구 도산대6길 (지민최리)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (28, 27, 44, '경기도 청주시 상당구 서초대가');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (29, 4, 62, '울산광역시 금천구 학동가 (시우강면)');
INSERT INTO OUTBOUND_REQUEST (id, product_id, quantity, target_address) VALUES (30, 4, 90, '부산광역시 송파구 언주로 (성진김최면)');


-- Inserts for WAREHOUSE_TEMPERATURE_LOG
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (1, 4, 15.0, '2025-04-20T15:44:18');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (2, 1, -18.5, '2025-02-15T16:18:07');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (3, 5, -14.0, '2025-02-06T15:55:04');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (4, 3, -15.3, '2025-03-25T18:06:54');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (5, 5, 9.2, '2025-02-12T00:25:36');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (6, 2, -2.7, '2025-02-21T11:43:21');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (7, 4, -15.0, '2025-04-10T21:08:39');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (8, 1, 7.5, '2025-02-02T12:11:22');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (9, 4, 7.6, '2025-04-09T23:10:25');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (10, 3, 9.3, '2025-01-28T05:41:01');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (11, 5, -6.7, '2025-04-26T23:55:07');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (12, 2, 23.1, '2025-04-11T11:10:52');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (13, 4, 16.0, '2025-04-14T06:02:59');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (14, 1, 3.4, '2025-04-09T07:36:09');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (15, 2, -18.3, '2025-05-06T17:25:18');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (16, 4, 0.0, '2025-04-03T09:38:58');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (17, 2, 0.5, '2025-03-08T08:30:29');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (18, 1, 23.0, '2025-02-20T06:54:26');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (19, 1, 1.9, '2025-03-26T03:53:58');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (20, 1, 22.5, '2025-02-28T13:47:47');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (21, 5, 5.7, '2025-03-30T14:05:00');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (22, 2, -9.7, '2025-05-07T07:41:55');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (23, 5, 11.4, '2025-04-13T06:42:56');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (24, 4, 2.8, '2025-01-11T03:04:10');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (25, 1, -13.4, '2025-05-22T15:45:24');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (26, 5, -1.0, '2025-02-07T17:36:59');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (27, 1, 22.1, '2025-02-19T21:40:07');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (28, 4, -17.2, '2025-03-31T02:14:41');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (29, 4, 14.8, '2025-05-15T06:02:16');
INSERT INTO WAREHOUSE_TEMPERATURE_LOG (id, warehouse_id, temperature, measured_at) VALUES (30, 1, 15.6, '2025-01-14T08:30:07');

-- Inserts for WORK_LOG
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (1, 14, '2025-01-04T13:11:03', '2025-01-04T20:11:03');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (2, 1, '2025-02-09T01:55:21', '2025-02-09T09:55:21');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (3, 30, '2025-04-26T23:06:19', '2025-04-27T05:06:19');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (4, 24, '2025-01-21T01:00:44', '2025-01-21T06:00:44');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (5, 3, '2025-02-19T18:41:40', '2025-02-20T00:41:40');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (6, 3, '2025-01-01T04:53:26', '2025-01-01T06:53:26');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (7, 12, '2025-02-12T01:20:56', '2025-02-12T02:20:56');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (8, 12, '2025-02-05T22:10:36', '2025-02-06T04:10:36');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (9, 6, '2025-05-01T00:48:15', '2025-05-01T01:48:15');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (10, 27, '2025-02-03T16:58:17', '2025-02-03T20:58:17');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (11, 27, '2025-05-09T09:49:29', '2025-05-09T15:49:29');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (12, 3, '2025-03-11T23:29:35', '2025-03-12T02:29:35');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (13, 7, '2025-04-29T07:59:58', '2025-04-29T08:59:58');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (14, 7, '2025-02-21T23:27:09', '2025-02-22T01:27:09');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (15, 24, '2025-01-02T17:59:34', '2025-01-02T18:59:34');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (16, 10, '2025-05-11T23:23:44', '2025-05-12T05:23:44');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (17, 23, '2025-05-08T12:17:03', '2025-05-08T13:17:03');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (18, 30, '2025-01-04T04:50:59', '2025-01-04T08:50:59');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (19, 28, '2025-01-05T00:14:25', '2025-01-05T03:14:25');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (20, 6, '2025-04-28T23:29:03', '2025-04-29T07:29:03');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (21, 4, '2025-02-02T12:30:16', '2025-02-02T20:30:16');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (22, 12, '2025-03-02T06:34:50', '2025-03-02T11:34:50');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (23, 5, '2025-02-15T20:28:42', '2025-02-15T21:28:42');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (24, 7, '2025-03-18T07:44:05', '2025-03-18T13:44:05');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (25, 11, '2025-04-08T01:16:00', '2025-04-08T09:16:00');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (26, 10, '2025-05-24T08:54:32', '2025-05-24T13:54:32');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (27, 30, '2025-03-12T12:59:53', '2025-03-12T18:59:53');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (28, 6, '2025-03-10T21:06:21', '2025-03-10T23:06:21');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (29, 4, '2025-01-18T01:13:52', '2025-01-18T06:13:52');
INSERT INTO WORK_LOG (id, member_id, check_in, check_out) VALUES (30, 6, '2025-04-24T00:40:01', '2025-04-24T07:40:01');

commit;

/* DML이므로 COMMIT; 해야 변경 사항 DB에 반영됨 */