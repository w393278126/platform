/*
Navicat MySQL Data Transfer

Source Server         : dev
Source Server Version : 50639
Source Host           : 192.168.0.151:3306
Source Database       : tour

Target Server Type    : MYSQL
Target Server Version : 50639
File Encoding         : 65001

Date: 2018-08-11 15:32:10
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_tour_user
-- ----------------------------
DROP TABLE IF EXISTS `t_tour_user`;
CREATE TABLE `t_tour_user` (
  `id` varchar(18) NOT NULL COMMENT 'id',
  `username` varchar(50) DEFAULT NULL COMMENT '姓名',
  `qq_number` varchar(20) DEFAULT NULL COMMENT 'QQ号码',
  `unionid` varchar(32) DEFAULT NULL COMMENT '微信unionid用户唯一ID',
  `wechat` varchar(255) DEFAULT NULL COMMENT '微信名',
  `password` char(128) DEFAULT NULL COMMENT '密码',
  `nick_name` varchar(255) DEFAULT NULL COMMENT '昵称',
  `sex` char(1) DEFAULT NULL COMMENT '性别： F-女，M-男',
  `real_name` varchar(255) DEFAULT NULL COMMENT '真实姓名',
  `mobile` varchar(255) DEFAULT NULL COMMENT '手机',
  `address` varchar(255) DEFAULT NULL COMMENT '地址',
  `nationality` varchar(20) DEFAULT NULL COMMENT '国籍',
  `passport` varchar(50) DEFAULT NULL COMMENT '护照号',
  `birthday` char(10) DEFAULT NULL COMMENT '出生日期',
  `source` varchar(10) DEFAULT NULL COMMENT '用户来源： 1-用户名登陆，2-QQ登陆，3-微信登陆',
  `picture_url` varchar(255) DEFAULT NULL COMMENT '用户头像URL',
  `status` char(1) DEFAULT NULL COMMENT '状态：0-无效，1-有效',
  `create_by` int(11) DEFAULT NULL COMMENT '创建人',
  `create_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '创建时间',
  `modify_by` int(11) DEFAULT NULL COMMENT '修改人',
  `modify_time` timestamp NULL DEFAULT '0000-00-00 00:00:00' COMMENT '修改时间',
  `deviceID` varchar(50) DEFAULT NULL COMMENT '设备唯一标识',
  `last_login_time` timestamp NULL DEFAULT '0000-00-00 00:00:00',
  `token` varchar(50) DEFAULT NULL COMMENT '会话标识',
  `smsCode` varchar(50) DEFAULT NULL,
  `last_smscode_time` timestamp NULL DEFAULT '0000-00-00 00:00:00',
  `invitation_code` varchar(5) DEFAULT NULL COMMENT '邀请码',
  `father_code` varchar(5) DEFAULT NULL COMMENT '谁邀请的',
  `followNum` int(11) DEFAULT NULL COMMENT '关注数量',
  `fansNum` int(11) DEFAULT NULL COMMENT '粉丝数',
  `praisedNum` int(11) DEFAULT NULL COMMENT '被点赞数',
  `dynamicNum` int(11) DEFAULT NULL COMMENT '发布的动态数',
  `idFace` varchar(255) DEFAULT NULL COMMENT '身份证正面',
  `idBack` varchar(255) DEFAULT NULL COMMENT '身份证反面',
  `city` varchar(8) DEFAULT NULL COMMENT '城市',
  `prvinice` varchar(8) DEFAULT NULL COMMENT '省份',
  PRIMARY KEY (`id`),
  UNIQUE KEY `mobile` (`mobile`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='旅游用户表';

-- ----------------------------
-- Records of t_tour_user
-- ----------------------------
INSERT INTO `t_tour_user` VALUES ('123456', '李四', null, null, null, null, '啦啦啦', 'M', null, 'j1tFpISWvZ1W6/jiYUR2Wg==', null, null, null, '1999-12-20', null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', null, null, '2018-08-06 09:38:02', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, null, '0000-00-00 00:00:00', null, null, '0', '0', '0', '0', 'https://s1.tuchong.com/content-image/201805/76d78f7e2326a235d19c0dd7f55a1690.png', 'https://s1.tuchong.com/content-image/201805/f1a20558c26d8e0e4644358b7804db45.jpeg', null, null);
INSERT INTO `t_tour_user` VALUES ('459370099799887872', '诱惑', null, null, null, null, '1234', 'M', null, 'Uf/PSlIFcE8kg6i0O9z0ew==', null, null, null, '2018-08-27', null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-06 09:38:00', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, '7325', null, null, null, '0', '0', '10', '1', 'http://192.168.1.11:8000/group2/M00/00/00/wKgBD1tfvI2AdyxfAAE3QOAYgs4393.png', 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDFtfvI2ALRQbAABltyiA9JM852.png', null, null);
INSERT INTO `t_tour_user` VALUES ('459374844539179008', '李四', null, null, null, null, null, 'M', null, 'oRtO7QbIqqYB3j4YvZ08tQ==', null, null, null, '2018-01-01', null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-09 18:15:57', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, '3624', '0000-00-00 00:00:00', null, null, null, null, null, null, null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('459375165277605888', null, null, null, null, null, null, null, null, 'fZLVrTMYmeimXOFcLkKmaA==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-03 14:25:49', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, '6273', '0000-00-00 00:00:00', null, null, null, null, null, null, null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('459379687676841984', null, null, null, null, '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', null, null, null, 'zDbn2kfOJUpw5KgrohIZSA==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '1', null, '2018-08-03 14:25:46', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, '9564', '0000-00-00 00:00:00', null, null, null, null, null, null, null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('463657595446628352', null, null, null, null, null, null, null, null, 'uWzVdv3THnqENQZMLXVk4g==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-03 14:25:45', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, '9920', '2018-07-03 11:11:03', null, null, null, null, null, null, null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('463716287688871936', null, null, null, null, 'c8c5d4cc030cb505400eb6814b77a95e54d23a2466e2f6676c6455fcb142ed00', null, null, null, 'RbFK9B6m0goHCanMptCrjw==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '1', null, '2018-08-03 14:25:44', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, '3385', '2018-07-03 16:33:51', null, null, null, null, null, null, null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('464741159172968448', '二小', null, null, null, null, null, null, null, 'b9UEgWi+Df2QEKI/bFTZng==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-05 14:53:43', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, '9334', '2018-07-06 10:39:32', null, null, null, null, null, null, null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('464746734761938944', '张三', null, null, null, '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', null, null, null, 'UG0ir+MPcgTjCJ1PbwG8tQ==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '1', null, '2018-08-05 14:53:34', null, '0000-00-00 00:00:00', '00000000-49f7-fdb7-0000-0000179ec14f', '0000-00-00 00:00:00', '8C0B62EC99464B83835A405CA2466228', '2622', '2018-07-14 14:55:53', '', null, null, null, null, null, null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('464747873209290752', '王lu', null, null, null, '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', '1234', 'M', null, 'Bg97BnDKg/rF6njxq9Jhkg==', null, null, null, '2018-08-27', null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-10 11:49:14', null, '2018-06-12 11:22:34', 'ffffffff-a2d2-67af-0000-0000179ec14f', '2018-06-12 11:22:34', null, '6602', '2018-08-10 11:49:13', null, null, '0', '0', '7', '47', 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltfwfCAGcHeAAE3QOAYgs4502.png', 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDVtfwfCAd1yAAABltyiA9JM708.png', null, null);
INSERT INTO `t_tour_user` VALUES ('467342037256114176', '王五', null, null, null, null, null, null, null, 'Nrz/I8D7uDpT/2yD5GpRKg==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-09 16:34:35', null, '0000-00-00 00:00:00', '00000000-49f7-fdb7-0000-0000179ec14f', '0000-00-00 00:00:00', null, '8703', null, null, null, '0', '1', '20', '15', null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('467714558400794624', '管理员007', null, null, null, '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', null, null, null, 'UPlYzGpYjR9fFmsLnJIClw==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '1', null, '2018-08-08 16:42:46', null, '0000-00-00 00:00:00', '00000000-49f7-fdb7-0000-0000179ec14f', '0000-00-00 00:00:00', '35F92955EC2C441696E4A9C1951181F6', '2289', null, 'NRZV', null, '0', '1', '15', '88', 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDVti1_KAa87rAABImOVJm04449.png', 'http://192.168.1.11:8000/group2/M00/00/00/wKgBD1ti1_KAVq8iAABZWBNeK_A492.png', null, null);
INSERT INTO `t_tour_user` VALUES ('471272150662254592', '建平', null, null, null, '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', null, null, null, 'Bg97BnDKg/rF6njxq9Jhkg2==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '1', null, '2018-08-06 11:12:13', null, '0000-00-00 00:00:00', '00000000-49f7-fdb7-0000-0000179ec14f', '0000-00-00 00:00:00', '59D9C814F9A44956B77CC78BB17443FF', '1534', '2018-08-02 10:06:58', null, null, '0', '0', '0', '0', null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('474674425833328640', '杨菲1', null, null, null, null, null, null, null, '1CIGO6uMHkuOPq5HcEcvxw==', null, null, null, null, null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-10 14:10:52', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, '2223', '2018-07-04 11:06:05', null, null, '1', '0', '0', '0', null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('476335394296827904', '舒畅', null, null, null, '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', '小小', 'F', null, 'AOcwXdhdMbCJlwXdsM1gDw==', null, null, null, '1980-12-01', null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-10 17:00:07', null, '0000-00-00 00:00:00', null, '0000-00-00 00:00:00', null, null, '0000-00-00 00:00:00', null, null, '0', '0', '0', '1', null, null, null, null);
INSERT INTO `t_tour_user` VALUES ('477840625551675392', null, null, '201808111146', null, null, '小磊', 'F', null, 'KHjqmCg1P4OzetwlWoiYVA==', '浦东新区罗山路', null, null, '1980-12-12', null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '1', null, '2018-08-11 15:31:15', null, '0000-00-00 00:00:00', 'ffffffff-a2d2-67af-0000-0000179ec15f', '0000-00-00 00:00:00', null, '5953', '2018-08-11 14:08:03', null, null, null, null, null, null, null, null, '上海', '上海');
INSERT INTO `t_tour_user` VALUES ('477861718152843264', null, null, '201808111530', null, null, '小李', 'M', null, '15974494956', '浦东新区罗山路222', null, null, '1992-12-12', null, 'http://192.168.1.11:8000/group2/M00/00/00/wKgBDltj8sGAf6g0AAA_01uMMy4660.png', '0', null, '2018-08-11 15:31:53', null, '0000-00-00 00:00:00', 'ffffffff-a2d2-67af-0000-0000179ec15f', '0000-00-00 00:00:00', null, '2750', '2018-08-11 15:31:52', null, null, null, null, null, null, null, null, '上海', '上海');
SET FOREIGN_KEY_CHECKS=1;
