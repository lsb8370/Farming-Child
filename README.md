# Farming Child 
캐주얼 농장 경영 게임 "자식 농사"

## 개요
Farming Child는 농사를 짓고 마을에서 주최하는 대회에도 참여하며 살아가는 캐주얼 농장 경영 게임입니다.

```
1. 타이틀 : 자식 농사

2. 장르 : 캐주얼 농장 경영 게임

3. 플랫폼 : PC

4. 타겟 플레이어 : 캐주얼 경영 게임을 좋아하는 플레이어
```

## 기술적 특징
+ 비동기(Asynchronous) 씬 전환
```
동기(Synchronous)로 씬 전환을 하면 씬을 전환하는 동안 프리징 현상이 나타납니다.

이는 유저에게 게임이 멈춘 것 같은 느낌을 주며 다음 장면으로의 씬 전환이 매끄럽지 못하다는 느낌을 줍니다.

이를 막기위해 비동기 씬 전환을 사용했으며 씬을 전환하는 동안 로딩화면을 보여주었습니다.
```
+ Json Format을 이용한 Save & Load 기능 구현
```
게임을 저장하기 위해서 다양한 Format 중 Json을 사용했습니다. 

Save를 했을 때 현재 재화, 맵의 상태 등이 Json으로 저장되며 게임 시작 시 Continue를 선택하면 이를 파싱하여 불러옵니다.
```
+ Event-Driven 구조
```
원래는 Event가 발생할 때까지 기다리는 동기적 방법을 사용하였지만 Event-Driven 구조를 도입하여 

비동기적으로 Event를 처리하게 했고 이를 통해 성능을 개선하고자 했습니다.
```
+ Behavior Tree를 사용한 AI 구현
```
Unity Asset Store에 있는 Behavior Tree 프레임워크 "Behavior Bricks"를 사용하여 AI를 구현했습니다.

https://assetstore.unity.com/packages/tools/visual-scripting/behavior-bricks-74816#description
```

## 저작권
+ 폰트 : 고도 M
```
NHN고도 폰트의 지적재산권은 NHN고도에 있습니다.

NHN고도 폰트는 개인 및 기업 사용자를 포함한 모든 사용자에게 무료로 제공되며,
특별한 허가 절차 없이 모든 매체에서 상업적으로 사용 가능합니다.

자유롭게 마음껏 수정 및 재배포 가능합니다.

저작권 안내와 라이선스 전문을 토함하여 다른 소프트웨어와 번들하여 재배포 또는
판매 가능합니다. 단, 글꼴자체의 유료 판매는 금지합니다.

NHN고도 폰트를 사용한 인쇄물, 게임, 광고물(온라인 포함) 등의 이미지는 NHN고도 및
서체 홍보를 위해 활용될 수 있으며, NHN고도 폰트 사용 시 이러한 활용에 동의하는 것으로
간주됩니다.
```

+ 아이콘 : 메뉴와 마을 이장님 캐릭터
```
[마을 이장님 캐릭터]
<div>아이콘 제작자 <a href="https://www.flaticon.com/kr/authors/photo3idea-studio" title="photo3idea_studio">photo3idea_studio</a> from <a href="https://www.flaticon.com/kr/" title="Flaticon">www.flaticon.com</a></div>


[메뉴]
<div>Icons made by <a href="https://www.flaticon.com/authors/smashicons" title="Smashicons">Smashicons</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>

<div>Icons made by <a href="https://www.flaticon.com/authors/freepik" title="Freepik">Freepik</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>
```

## 유튜브 영상
https://youtu.be/JRGmQSnVbEA
