## 三维五子棋

引言：只是用来无聊练练，以后可以做成联机版会好玩一点

### 操作说明

鼠标右键按住拉动视角，鼠标滑轮滑动来拉远拉近视角，按住滑轮可以平移视角。

鼠标左键下棋，下完后自动换执棋者。

### 胜利条件

从任意方向同色棋子连成线即可获胜。

### Unity版本

Unity2018.3.4f1

### 实现框架

##### 两个场景Scene

第一个：开始界面，最基础的场景搭建，Update函数中构建GUI Button，并跳转下一个场景

第二个：游戏界面。

用到的Prefab：

| Prefab   | 表示           | Component                                                    |
| -------- | -------------- | ------------------------------------------------------------ |
| Ball     | 用于表示棋子   | 这里需要微调形状以及SphereColiider的形状 + Rigidbody（用于下落，这个constrain要配套限制棋子不能左右移动，只能上下移动） |
| Cylinder | 表示下棋的柱子 | 套用的Cylinder + Capsule Collider + Cylinder.cs(自己实现) + Highlightable Object（高亮系统配件） |
| Plane    | 表示棋盘       | 修改material就好                                             |

场景中的GameObject：

| GameObject | 功能介绍                                                     |
| ---------- | ------------------------------------------------------------ |
| MainCamera | 注意配套HighLight Effect（高亮系统）+ Camera Scripts.cs（响应鼠标滑动视角）+ Information.cs（显示当前棋子手以及胜方） |
| Plane      | 初始化平面                                                   |
| Laucher    | Empty Object + Laucher.cs （初始化以及调度）                 |

### 程序逻辑

Laucher.cs启动，其中初始化设定好的棋柱（Cylinder）数量，保存在一个数组中。在下棋的时候由Cylinder的鼠标进入点击来判断是否下棋，若当前可下棋，则把自己的身份信息传给Laucher总控制台，Laucher调用MakeStep来进行一步操作，更新当前的步数显示信息以及执棋手信息到屏幕中，Laucher添加棋子的方法其实就是到数组中找对应实例的Cylinder调用其AddCheese方法，Laucher接着由此进行EndGame检测，从当前下的棋子出发，从各个方向检测，若游戏结束，则将Cylinder.cs的ready参数设为false导致不能再请求调用Laucher的MakeStep。结束时实例化GameOver.cs进行新建按键，达到界面的跳转交互。

### 部分功能逻辑实现

1.高亮系统：使用了Highlight Effect插件，直接在Camera上挂载Highlight Effect，在需要高亮的物体上挂载Highlight Object，在MouseEnter的时候进行显示即可。

2.摄像头视角转换：涉及到各个坐标的转换问题，建议再找相关的资料深入理解，主要是修改camera.tranform.position和rotation来改变摄像机的位置达到视角转换的效果，具体看代码。

3.有关游戏暂停的事考虑了Time.timescale = 0来处理，但这个无法避免下棋继续，所以还是考虑用ready变量来限制。

### 效果

![](gif\Learn3DUI.gif)

![](gif\Learn3Dgame.gif)

### 可以改进部分：

- [ ] 联机部分
- [ ] 执棋手更加明显
- [ ] 棋子判定是否有bug
- [ ] 地图系统可以做的更好