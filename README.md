# 📦 FreshBox
> **공장의 자동화를 위한 식품 유통 관리 및 사원 관리 시스템**

<br />  

## 🛠️ 개발 스택

- **언어:** C#, XAML
  
- **프레임워크:** .NET WPF
- **디자인 패턴:** MVVM 패턴
- **데이터베이스:** MySQL
- **버전 관리:** Git, GitFlow 브랜치 전략
- **툴:** Visual Studio, MySQL Workbench
- **라이브러리:**
  - CommunityToolkit.Mvvm
    - MVVM 패턴 구현을 지원하는 라이브러리
  - BCrypt.Net-Next (v4.0.3)
    - 비밀번호 해싱 라이브러리로, 안전한 암호 저장과 인증에 사용(BCrypt 알고리즘을 기반)
  - LiveChartsCore.SkiaSharpView.WPF
    - WPF 환경에서 고성능 차트 렌더링을 지원하는 라이브러리
  - MaterialDesignColors
    - Material Design 컬러 팔레트를 제공하여 UI 디자인 시 일관된 색상 사용에 도움
  - MaterialDesignThemes
    - Material Design 스타일의 WPF 테마 및 컨트롤 세트를 제공하여 깔끔하고 현대적인 UI 구현에 도움
  - Microsoft.Xaml.Behaviors.Wpf
    - XAML에서 행동(Behavior)과 트리거(Trigger)를 사용할 수 있게 하는 라이브러리(MVVM 패턴에서 UI와 로직 분리에 유용)
  - MySql.Data
    - MySQL 데이터베이스와 .NET 애플리케이션 간 연결 및 조작을 지원하는 공식 라이브러리
  - System.Configuration.ConfigurationManager
    - 앱 설정 파일(App.config, Web.config)을 읽고 쓰는 데 사용

<br />  

## ❔ 개발 동기 (Why?)

1. 출퇴근 기록의 불편함 개선  
2. 물류에 필요한 자동화 관리 (사용자의 편리성)  
3. 스마트팩토리 관련 프로젝트 경험을 위해  

<br />

## ❔ 프로젝트 목적 (What?)

1. 재고 관리를 좀 더 편하게 하고  
2. 출퇴근 관리를 용이하게 하기 위해  

<br />

## ❔ 주요 기능 (How?)

1. 회원 가입 → 사원과 관리자를 분리  
2. 상품 등록  
3. 사용자 편의성을 고려한 편리한 UX/UI  
4. 입고 정보 관리  

<br />

## 👤 타겟층
쇼핑몰, 유통회사 등에서 회원 및 재고 관리를 담당하는 공장주, 관리자, 사원 등 실무자  

<br />  

## 🖍️ UI 설계

[화면 설계서 - Notion 링크](https://www.notion.so/20c9879dac8681378f28e48e6a7346c7)  

<br />

## 🗂️ 폴더 구조 (CommunityToolkit.Mvvm 라이브러리 적용)

```jsx
FreshBox/
├── Assets/
├── Converter/
├── Database/
├── DTOs/
├── Enums/
├── Interfaces/
├── Models/
├── Repository/
├── Services/
├── ViewModels/
├── Views/
└── Dependencies/
```

<br />

## 📘 주요 기능 상세 설명
### 회원 관리
- 회원 가입  
  - 중복 검사  
  - 유효성 검사  
  - 비밀번호 암호화
  - DB INSERT  
- 로그인  
- 로그아웃  

### 상품 관리
- DB 샘플 데이터 SELECT 후 UI 바인딩  
- 등록 버튼 클릭 시 DB INSERT  

### 주문 및 입고 관리
- 주문 내역 조회  
- 입고 처리  

### UI 및 네비게이션
- 로그인 사용자 정보 메인 화면 출력  
- 메인 화면 내 동적 페이지 전환 (단일 MainWindow 기반)  
- `INavigationService` 인터페이스 정의  
- `NavigationService` 클래스 구현 및 싱글턴 패턴 적용  
- 권한에 따른 버튼 가시성 처리

<br />  

## 협업 방식

### GitFlow 브랜치 전략 및 PR 활용
```
💡main ← 최종 배포용, 항상 안정적
└─ develop ← 개발 통합본
├─ feature/기능명 ← 기능별 작업
├─ fix/버그명 ← 버그 수정
├─ hotfix/긴급패치 ← 운영 중 긴급 패치
└─ release/버전명 ← 배포 준비
```

<br />  

### 역할 분담

| 이름   | 역할 및 담당 분야                             |
| ------ | -------------------------------------------- |
| 박현승 | 발표, 디자인(로고·와이어프레임), 프론트엔드 UX/UI 디자인 |
| 서예경 | 리더, 프로젝트 생성 및 버전 관리, 백엔드 상품·재고 관리   |
| 최마리 | 산출물 문서 작성 및 관리, 백엔드 회원·출퇴근 관리        |

<br />  

## 트러블 슈팅 및 오류 해결

| 이름   | 작업 내용 및 문제점  | 해결 방법   |
| ------ | ---------------------------------------- | --------------------------------------------|
| 박현승 | Main Page View UI 설계 후 XAML 속성 지식 부족| 태그 속성 학습 및 코드 정리, 코너 라운드 속성 등 문제 해결 |
| 서예경 | RelayCommand 참조 실패, DataContext 미설정 에러| XAML 및 코드비하인드에 ViewModel 참조 선언 추가 |
| 최마리 | PasswordBox MVVM 바인딩 문제 (비밀번호 보안 이슈)| 코드비하인드에서 ViewModel로 직접 값 전달,<br>AttachedProperty 시도 후 포기 |

<br />  

## 결과 및 느낀점

| 이름   | 어려웠던 점 및 해결 방법          | 개선점 및 느낀점        |
| ------ | ------------------------------------ | ------------------------------------------ |
| 박현승 | 기본 코딩 개념 부족, 직접 구현 미흡 → 지속적 학습 필요| 단기간 구현의 한계 경험,<br>팀원 간 소통과 체력 분배 중요성 체감|
| 서예경 | MySQL 초기 연결 문제 → 재설치 및 재연결로 해결,<br>MVVM 구조화 어려움 → 예시 코드 참고하여 공부,<br>DataContext 참조 문제→ 클래스 접근자 변경 및 클린 솔루션 시도로 해결| 기능 집중 위한 시간 확보 필요,<br>팀장으로서 역할 및 목표 설정 중요성 인지|
| 최마리 | MySQL 연동 초기 시간 소모, 동기 메서드 위주 구현이 아쉬움 <br>→ 코드 구조가 잡힌 이후부터는 순조롭게 진행| 의존성 주입 적용 필요, 비동기 메서드로 변경,<br>개발 기간 부족 아쉬움|

<br />  

## 문의 및 연락처

- 팀 리더: 서예경  
- 이메일: seoyekyung@example.com

  
<br />  

