# 体育赛事 库表/Redis 说明

## 库表

### 体育档期:`SportSchedules`

|字段名|类型|说明|
|----|----|----|----|
|Id|INT(11)|排期编号|
|GameId|INT(11)|游戏编号|
|Title|VARCHAR(200)|标题|
|Start|DATE|开始日期|
|End|DATE|结束日期|
|SportType|INT(3)|比赛类型 `1`: 对抗赛 |
|Status|INT(3)|记录状态 `-1`:删除 `1`:创建|
|CreateAuthorId|INT(11)|创建人|
|CreateTime|DATETIME|创建时间|
|LastAuthorId|INT(11)|最后编辑人|
|LastUpdateTime|DATETIME|最后更新时间|



### 档期赛事: `SportMatchs`

|字段名|类型|说明|
|----|----|----|----|
|Id|INT(11)|赛事编号|
|GameId|INT(11)|游戏编号|
|ScheduleId|INT(11)|档期编号|
|StreamAddress|VARCHAR(500)|流地址|
|Start|DATETIME|开始时间|
|End|DATETIME|结束时间|
|RoomAId|INT(11)|A队房间编号|
|RoomAScore|INT|A队得分|
|RoomBId|INT(11)|B队房间编号|
|RoomBScore|INT|B队得分|



### 体育回放排期 `SportPlayBackSchedules`

|字段名|类型|说明|
|----|----|----|----|
|Id|INT(11)|回放编号|
|GameId|INT(11)|游戏编号|
|RoomId|INT(11)|房间编号|
|Title|VARCHAR(200)|标题|
|ReportPlayTimeId|INT(11)|回放录像编号|
|RecordingTime|DateTime|录像时间|
|Duration|INT|录像时长(秒)|
|VideoState|INT(3)|录像开启关闭状态 `0`:关闭 `1`:开启|
|Status|INT(3)|记录状态 `-1`:删除 `1`:创建|
|SortNum|INT(8)|排序号|
|CreateAuthorId|INT(11)|创建人|
|CreateTime|DATETIME|创建时间|
|LastUpdateTime|DATETIME|最后更新时间|
|LastAuthorId|INT(11)|最后编辑|

## Redis

### 房间对阵信息

|-|-|
|----|----|
|KEY|`sport:room:vsinfo`|
|TYPE| Hash|
|FIELD| RoomId|
|VALUE| { RoomA:{ Id,Domian,Logo,Name}, RoomB:{Id,Domian,Logo,Name } }|
|REMARK| 开始直播清除，重新打入|

### 用户阵营信息

|-| -|
|----|----|
|KEY|`sport:room:{RoomId}:user`|
|TYPE| Hash|
|FIELD| UserId|
|VALUE| RoomId|
|REMARK| 结束直播清除|

### 阵营积分

|-|-|
|----|----|
|KEY| `sport:room:points`  |
|TYPE| Hash|
|FIELD| RoomId|
|VALUE| points|
|REMARK| 开始直播清除|

### 排行榜

|-|-|
|----|----|
|KEY| `sport:room:{RoomId}:rank`|
|TYPE| SortSet|
|FIELD| UserId|
|VALUE| points|
|REMARK| 开始直播清除|

### 体育房间回放标题
|-|-|
|----|----|
|KEY| `sport:room:playback:title`|
|TYPE| Hash|
|FIELD| RoomId|
|VALUE| title|
|REMARK| 用于设置回放房间标题|

### 房间开播比赛
|-|-|
|----|----|
|KEY| `sport:room:match`|
|TYPE| Hash|
|FIELD| RoomId|
|VALUE| MatchId|
|REMARK| 开始直播时写入|

### 比赛队伍分数
|-|-|
|----|----|
|KEY| `sport:match:score`|
|TYPE| Hash|
|FIELD| MatchId|
|VALUE| "A_Score,B_Score" |
|REMARK| 后台编辑保存写入 |