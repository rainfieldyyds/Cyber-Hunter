# AGENTS.md — cyberpunk（Unity 2D 动作游戏工程）

> 本文件面向在此仓库工作的代码 Agent 与协作者。以下内容均基于当前工程文件逐项核对，最近一次更新：2026-09-07（SampleScene 第一阶段整理后）。若后续引擎版本、包或目录发生变化，请同步更新本文件。

## 1. 项目信息

### 1.1 引擎与工程配置

- **Unity 版本**：2022.3.62f1c1（2022.3 LTS 分支），记录于 `ProjectSettings/ProjectVersion.txt`。请使用该版本打开工程，避免意外升级。
- **包注册源**：`Packages/packages-lock.json` 中的包均来自 `https://packages.unity.cn`。
- **渲染管线**：内置渲染管线（Built-in Render Pipeline）。`Packages/manifest.json` 中没有 URP/HDRP 相关包，`ProjectSettings/GraphicsSettings.asset` 中 `m_CustomRenderPipeline: {fileID: 0}`；默认渲染路径为 Forward（`m_DefaultRenderingPath: 1`），相机未单独覆盖。
- **色彩空间**：Linear（`m_ActiveColorSpace: 1`）。
- **输入系统**：仅启用旧版 Input Manager（`activeInputHandler: 0`），未安装 `com.unity.inputsystem`。代码统一使用 `Input.GetAxisRaw / Input.GetKey / Input.GetMouseButton` 等旧 API，不要混入新 Input System API。
- **产品信息**：`productName: cyberpunk`、`companyName: DefaultCompany`；默认分辨率 1920×1080（全屏模式 `fullscreenMode: 1`）。
- **游戏形态**：从场景和脚本判断为横版 2D 动作/平台跳跃玩法：近战攻击/防御、敌人巡逻、Boss 战、存档点/复活、对话、暂停/设置/GameOver 等 UI。

### 1.2 构建场景

`ProjectSettings/EditorBuildSettings.asset` 中启用的场景及顺序为：

1. `Assets/Scenes/title.unity`（标题/开始界面）
2. `Assets/Scenes/SampleScene.unity`（主游戏关卡，含 Boss 战）

`Assets/Hero_Knight/Demo/Scene demo.unity` 是第三方素材自带的 Demo 场景，不在 Build Settings 中，也不应加入。

### 1.3 关键包

`Packages/manifest.json` 中的直接依赖（含内置模块）：

| 包 | 版本 | 用途/说明 |
|---|---|---|
| `com.unity.cinemachine` | 2.10.7 | Boss 战镜头切换（`BossCameraController`、Virtual Camera） |
| `com.unity.feature.2d` | 2.0.1 | 2D 功能集，聚合 2D Animation/Sprite/Pixel Perfect/PSD Importer/SpriteShape/Tilemap Extras/Aseprite 等 |
| `com.unity.textmeshpro` | 3.0.7 | UI 文本（对话、提示等使用 `TextMeshProUGUI`） |
| `com.unity.ugui` | 1.0.0 | uGUI UI 系统 |
| `com.unity.timeline` | 1.7.7 | 已安装，当前未发现依赖 Timeline 的游戏逻辑 |
| `com.unity.visualscripting` | 1.9.4 | 已安装，项目中未见 Visual Script 资产使用 |
| `com.unity.test-framework` | 1.1.33 | 测试框架已安装；`Assets` 下目前没有测试目录 |
| `com.unity.collab-proxy` | 2.12.4 | Unity 版本控制插件 |
| `com.unity.ide.rider` / `com.unity.ide.visualstudio` | 3.0.36 / 2.0.22 | IDE 集成 |

内置模块按需启用，实际游戏逻辑大量依赖 `physics2d`、`animation`、`audio`、`ui`、`tilemap`、`particlesystem`。项目中**没有** URP/HDRP、Input System、Addressables、DOTween 等依赖。

### 1.4 Assets 目录结构

Assets 顶层按中文资源类别组织（文件数为非 `.meta` 文件数）：

| 目录/位置 | 内容 |
|---|---|
| `Assets/脚本/` | 39 个 C# MonoBehaviour，全部游戏逻辑集中于此；无 asmdef、无 namespace |
| `Assets/Scenes/` | `title.unity`、`SampleScene.unity` |
| `Assets/avatar/` + `Assets/角色动画/` | 玩家角色序列帧与 Animator 动画剪辑（Attack/Idle/Run/Block/死亡等） |
| `Assets/boss/` + `Assets/boss动画/` | Boss 美术与动画 |
| `Assets/enemy/` + `Assets/enemy动画/`、`Assets/npc/` + `Assets/npc动画/` | 敌人/NPC 美术与动画 |
| `Assets/地面/` | `street1~4.png` 关卡路面图 |
| `Assets/background/` | 背景图（含 AI 生成图片） |
| `Assets/粒子/` | 刀光特效图与材质 |
| `Assets/ui/` | UI 贴图与标题动画 |
| `Assets/Font/` | 自定义字体 `Silver.ttf` 与 `Silver SDF.asset` |
| `Assets/music/` | BGM/SFX（mp3/wav，文件名即用途） |
| `Assets/` 根目录 | Animator Controller（`Avatar Controller`、`Boss Controller`、`Enemy Controller`、`Enemy Controller2`、`Npc Controller`、`Title Controller`）与 `master.mixer` 音频混合器 |
| `Assets/Editor/` | 编辑器整理工具 `SceneOrganizerTool.cs`（Tools → Scene Organizer 菜单，供后续场景整理复用） |
| `Assets/Hero_Knight/` | 第三方骑士素材包（含动画/精灵/Demo 场景） |
| `Assets/TextMesh Pro/` | TMP Essentials 导入资源（字体、着色器、设置） |

工程约含 60 张 PNG、22 个动画剪辑、8 个 WAV、5 个 MP3、7 个 Animator Controller、3 个场景、208 个 `.meta`。**当前 Assets 下没有任何 `.prefab` 文件，也没有 `.asmdef`。**

## 2. 代码规则

- **程序集与命名空间**：现有脚本全部位于 `Assets/脚本/`，无 asmdef，类均声明在全局命名空间。新 MonoBehaviour 默认放在 `Assets/脚本/`，类名与文件名一致，文件名与类名不匹配会无法挂载。若要把脚本拆分到 asmdef 或加入 namespace，属于结构性改动，须作为独立任务并验证场景中既有组件不受影响。
- **Unity 序列化**：场景/预制体通过 GUID + fileID 引用脚本，字段值按字段名序列化。删除 `.cs` 后重建或随意改类名/字段名会让 Inspector 引用丢失。改名时保留 `.meta` 与 GUID；改序列化字段名使用 `[FormerlySerializedAs]` 或先在编辑器里重新赋值。
- **字段风格**：现有代码混用 `public` 序列化字段与 `[SerializeField] private`，常用 `[Header("中文标题")]` 分组、中文注释。新代码建议用 `[SerializeField] private` + `[Header]`；不要批量改写已挂到场景的公开字段，避免误改序列化数据。
- **C# 风格**：类与公开方法用 PascalCase，私有字段用 camelCase；新增代码保持 4 空格缩进与 `{ }` 换行风格。现有部分脚本缩进混乱，改动相关文件时不要顺手做大范围格式化（会淹没真实改动）。
- **输入**：只使用旧版 Input Manager（见 1.1）。方向用 `Input.GetAxisRaw("Horizontal")`；交互键 `F`（NPC）、`Esc`（暂停）、鼠标左/右键（攻击/防御）、Shift（疾跑）等已有约定，新增按键先复用现有 InputManager 轴或 `KeyCode`。
- **2D 物理**：移动体是 `Rigidbody2D`，碰撞/触发用 `Collider2D` 回调（`OnTriggerEnter2D/OnTriggerExit2D`）。写速度/物理状态变更时优先放 `FixedUpdate`，`Update` 只做输入采样与逻辑判断（现有 `PlayerController.Move()` 在 `Update` 里直接写 `rb.velocity`，如需修复请单独验证手感）。
- **引用与查找**：组件在 `Start/Awake` 缓存一次（`GetComponent`），避免每帧重复查找；尽量用 Inspector 序列化引用或碰撞回调传参，不要用 `FindGameObjectWithTag`/`Find` 拼运行时依赖。
- **Tag 与 Layer**：自定义 Tag 为 `Boss`、`Enemy`、`Ground`；脚本普遍用 `other.CompareTag("Player")`。Layer 已定义 `Player`(6)、`Enemy`(7)、`Boss`(8)、`BossAttack`(9)、`EnemyAttack`(10)、`PlayerAttack`(11)、`PlayerDefense`(12)（配合命中框）。命中判定脚本同时使用 `GetComponent<...Health>`，改 Layer/Tag 时需同步检查这些脚本。
- **单例**：`RespawnManager.instance`（DontDestroyOnLoad + 防重）与 `GameStateManager.instance`（直接赋值，无防重）是场景内简单单例。新管理器沿用防重写法；不要向场景添加第二个 `RespawnManager`/`GameStateManager`。
- **动画事件**：受击框开关通过动画事件调用公开方法（如 `EnableHitBox/DisableHitBox/EndAttack/CheckHeavyAttack`），按方法名绑定。重命名这些方法必须同步修改 `.anim` 事件，否则攻击/受击逻辑静默失效。
- **UI**：使用 uGUI + TextMeshPro（勿新增旧版 `UnityEngine.UI.Text`）；`Time.timeScale` 会因暂停/GameOver 变为 0，需要继续计时的协程注意用 `unscaledDeltaTime`。
- **音频**：BGM 切换用协程淡入淡出，由场景内 Manager 驱动；音量经 `AudioManager` 写入 `master.mixer` 的 `MasterVolume`。不要复制常驻音频物体，避免场景切换后多份播放。
- **相机**：主相机挂载 Pixel Perfect Camera + Cinemachine Brain，并带有 `BossCameraController`。新增镜头行为时优先复用/扩展该 Virtual Camera，不要直接替换主相机对象。

## 3. 场景与 Prefab 注意事项

- **本项目目前没有 Prefab**：Boss、敌人、路面、背景等大量对象是直接放在 `SampleScene` 里的场景对象，且存在大量复制实例（如 `enemy1 (15)`、`street1 (12)`、`enemydied`）以及未命名子物体。不要假设同名对象有 Prefab 联动；改动前先确认目标引用。
- **SampleScene 已完成第一阶段层级整理（2026-09-07）**：顶层根物体收敛为 `_Player`、`_NPC`、`_Enemies`、`_Boss`、`_Environment`、`_Background`、`_Triggers`、`_UI`、`_Audio`、`_Systems`、`_Cameras` 分组容器；原 `enemys1`/`enemydieds` 系列容器已改名为 `Enemies_Active`/`Enemies_Dead*`。`GameManager`（RespawnManager，`DontDestroyOnLoad`）刻意保持为根物体，不得移入分组。后续场景改动请维持该层级约定。
- 新做可复用对象时优先创建 Prefab；若要把现有场景对象转为 Prefab，请在 Unity 编辑器内完成（Pack Prefab 并核对引用），不要在文本层手工迁移。
- **主相机组成**（`SampleScene`）：`Main Camera` 同时挂 AudioListener、正交 Camera、Pixel Perfect Camera（PPU=2、参考分辨率 1920×1080、像素吸附开启）、Cinemachine Brain、`BossCameraController`；子物体 `Virtual Camera`（Cinemachine）默认跟随玩家。`BossCameraController` 在 Boss 战切换其 `Follow` 到 `bossCameraPoint`，Boss 结束切回玩家。
- **Boss 战触发链**：玩家进入 `BossTrigger` → `BossStartTrigger` 一次性触发：`BossCameraController.StartBossCamera()`、`BossGateController.LockBossRoom()`（前后门关闭）、启用 `BossAI`、显示血条、切换 Boss 音乐、停止普通 BGM。Boss 死亡后 `BossHealth` 写 `GameStateManager.instance.bossDead` 并解锁房间。涉及组件互相以 Inspector 引用绑定，删对象/改字段前要沿着引用链检查。
- **玩家/存档链**：`PlayerSpawn` 从 `RespawnManager` 取复活点；`RespawnManager` 用 `DontDestroyOnLoad` 跨场景保留；关卡内有普通存档点与 Boss 房前 `Checkpoint`。`RestartButton` 重载当前场景，`SceneLoader` 按场景名加载（目标场景必须存在于 Build Settings）。
- **Tag/Layer 纪律**：标签和图层已按 1.1/2 节约定命名；新触发器用 `CompareTag` 区分玩家，命中框记得设置对应 Layer，否则现有受击脚本可能收不到事件。
- **UI 与字体**：UI 文本统一 TMP；自定义字体为 `Assets/Font/Silver SDF.asset`。新增文案尽量走现有 `DialogueSystem`/Hint UI 模式，不要在场景里散落硬编码文字对象。
- **第三方内容**：`Assets/Hero_Knight/` 与其 Demo 场景、`Assets/TextMesh Pro/` 包资源原则上不修改。

## 4. 工作流程

### 打开与验证

- 用 Unity 2022.3.62f1c1 打开工程根目录（`ProjectSettings`、`Assets`、`Packages` 所在层）。安装新包后确认 Package Manager 解析自 `packages.unity.cn`。
- 代码改动验收标准：Unity 无编译错误/Console 报错；从 `title` 进入 `SampleScene` 后基本流程可玩；涉及 Boss/存档/暂停的改动分别走一遍触发链。
- 本工程没有自动化测试与 CI；不要假设 `dotnet build` 等价于 Unity 编译，最终以 Unity 控制台为准。

### 修改代码

1. 新增脚本：在 `Assets/脚本/` 新建 `<类名>.cs`，类名与文件名一致；不要手写 `.meta`，交给 Unity 导入生成。
2. 改动已有脚本前，搜索类名/方法名在场景 YAML、`.anim`、`.controller` 中的使用（动画事件尤其危险）。
3. 需要新绑定（挂脚本、拖引用、加触发器、改 Inspector 字段）时，改动代码后应说明需要在编辑器中完成的绑定步骤，或由协作者在编辑器内完成。
4. 删除/重命名脚本或资源要同步处理 `.meta`，否则 GUID 变化导致场景引用丢失。

### 修改场景/Prefab

- `.unity/.prefab/.asset` 是文本 YAML，但对象关系靠 GUID/fileID 串联，**不要手工编辑结构或 GUID**；结构性修改一律通过 Unity 编辑器进行。
- `SampleScene` 对象多、副本多，避免多人同时编辑同一场景；提交前在编辑器中重新保存并确认没有多余 diff。
- 新场景加入 Build：在 `File > Build Settings` 添加并按顺序排列，`SceneLoader.sceneName` 填与 Build 一致的场景名。

### 版本控制

- 仓库根目录已有 `.gitignore`（排除 `Library/ Temp/ Obj/ Logs/ UserSettings/ Build/` 等生成物，详见该文件注释）。
- 需要提交：`Assets/`（含全部 `.meta`）、`Packages/manifest.json`、`Packages/packages-lock.json`、`ProjectSettings/`、`.gitignore`、`AGENTS.md`。
- 不需要提交：`.csproj/.sln`（Unity/IDE 生成）、`Library/ Temp/ Logs/ UserSettings/`、构建产物。
- 仓库已初始化并有基线提交；接入或提交时继续遵循上述目录约定，并确认生成目录未被加入索引。
