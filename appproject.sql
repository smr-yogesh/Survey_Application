-- MariaDB dump 10.17  Distrib 10.4.14-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: appproject
-- ------------------------------------------------------
-- Server version	10.4.14-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `administrator`
--

DROP TABLE IF EXISTS `administrator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `administrator` (
  `AdminKey` varchar(30) NOT NULL,
  PRIMARY KEY (`AdminKey`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
/*!40000 ALTER TABLE `administrator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `answers`
--

DROP TABLE IF EXISTS `answers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `answers` (
  `Question_ID` int(11) NOT NULL,
  `Answer_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Answer_Description` varchar(300) DEFAULT NULL,
  `respondent_ID` int(11) DEFAULT NULL,
  `answer_type` int(3) DEFAULT NULL,
  PRIMARY KEY (`Answer_ID`),
  KEY `respondent_ID` (`respondent_ID`),
  KEY `Question_ID` (`Question_ID`),
  CONSTRAINT `answers_ibfk_2` FOREIGN KEY (`respondent_ID`) REFERENCES `key_a` (`respondent_id`),
  CONSTRAINT `answers_ibfk_3` FOREIGN KEY (`Question_ID`) REFERENCES `questions` (`Question_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `answers`
--

LOCK TABLES `answers` WRITE;
/*!40000 ALTER TABLE `answers` DISABLE KEYS */;
INSERT INTO `answers` VALUES (10,1,'good',NULL,1),(11,2,'I\'m 21 years old.',NULL,3),(12,3,'Vollyball#Football#',NULL,2),(13,4,'Today is 14th Dec 2020.',NULL,3),(14,5,'Good',NULL,1);
/*!40000 ALTER TABLE `answers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `key_a`
--

DROP TABLE IF EXISTS `key_a`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `key_a` (
  `Respondent_ID` int(11) NOT NULL,
  `LogOutTimeDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Respondent_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `key_a`
--

LOCK TABLES `key_a` WRITE;
/*!40000 ALTER TABLE `key_a` DISABLE KEYS */;
INSERT INTO `key_a` VALUES (123123,NULL),(2147483647,NULL);
/*!40000 ALTER TABLE `key_a` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `manager`
--

DROP TABLE IF EXISTS `manager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `manager` (
  `ManagerKey` varchar(30) NOT NULL,
  PRIMARY KEY (`ManagerKey`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `manager`
--

LOCK TABLES `manager` WRITE;
/*!40000 ALTER TABLE `manager` DISABLE KEYS */;
/*!40000 ALTER TABLE `manager` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `manager_account`
--

DROP TABLE IF EXISTS `manager_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `manager_account` (
  `Manager_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(20) DEFAULT NULL,
  `Password` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`Manager_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `manager_account`
--

LOCK TABLES `manager_account` WRITE;
/*!40000 ALTER TABLE `manager_account` DISABLE KEYS */;
INSERT INTO `manager_account` VALUES (1,'Testinguser',NULL),(2,'Testinguser',NULL),(5,'IsThisWorking',NULL),(6,'id','pass'),(7,'yogesh','bhatt');
/*!40000 ALTER TABLE `manager_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `manager_survey`
--

DROP TABLE IF EXISTS `manager_survey`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `manager_survey` (
  `Manager_ID` int(11) NOT NULL,
  `Survey_ID` int(11) NOT NULL,
  KEY `Manager_ID` (`Manager_ID`),
  KEY `Survey_ID` (`Survey_ID`),
  CONSTRAINT `manager_survey_ibfk_1` FOREIGN KEY (`Manager_ID`) REFERENCES `manager_account` (`Manager_ID`),
  CONSTRAINT `manager_survey_ibfk_2` FOREIGN KEY (`Survey_ID`) REFERENCES `survey` (`Survey_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `manager_survey`
--

LOCK TABLES `manager_survey` WRITE;
/*!40000 ALTER TABLE `manager_survey` DISABLE KEYS */;
/*!40000 ALTER TABLE `manager_survey` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `page`
--

DROP TABLE IF EXISTS `page`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `page` (
  `Page_ID` varchar(11) NOT NULL,
  `Survey_ID` int(11) NOT NULL,
  `PageNumber` int(11) DEFAULT NULL,
  `PageTitle` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Page_ID`),
  KEY `Survey_ID` (`Survey_ID`),
  CONSTRAINT `page_ibfk_1` FOREIGN KEY (`Survey_ID`) REFERENCES `survey` (`Survey_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `page`
--

LOCK TABLES `page` WRITE;
/*!40000 ALTER TABLE `page` DISABLE KEYS */;
/*!40000 ALTER TABLE `page` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `questions` (
  `Question_ID` int(11) NOT NULL AUTO_INCREMENT,
  `QuestionDescription` varchar(300) DEFAULT NULL,
  `QuestionType` int(11) DEFAULT NULL,
  `Compulsory` int(11) DEFAULT NULL,
  `Options` varchar(500) DEFAULT 'null',
  `Page_number` int(11) DEFAULT NULL,
  `survey` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Question_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questions`
--

LOCK TABLES `questions` WRITE;
/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` VALUES (10,'how are you?',1,1,'bad#fine#good#',1,'One last test'),(11,'How old are you?(Write in text)',3,1,NULL,1,'One last test'),(12,'Which game you like most?(Multiple choice)',2,1,'Vollyball#Cricket#Football#Golf#',1,'One last test'),(13,'What is date?',3,1,NULL,1,'todaytest'),(14,'How are you feeling today?',1,1,'Bad#Fine#Good#',1,'todaytest');
/*!40000 ALTER TABLE `questions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `survey`
--

DROP TABLE IF EXISTS `survey`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `survey` (
  `Survey_ID` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  `Status` int(11) DEFAULT 0,
  `TargetGroup` varchar(50) DEFAULT NULL,
  `ExpireDate` date DEFAULT NULL,
  `StartDate` date DEFAULT NULL,
  PRIMARY KEY (`Survey_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `survey`
--

LOCK TABLES `survey` WRITE;
/*!40000 ALTER TABLE `survey` DISABLE KEYS */;
INSERT INTO `survey` VALUES (9,'One last test',0,NULL,NULL,'0000-00-00'),(10,'todaytest',0,NULL,NULL,'0000-00-00');
/*!40000 ALTER TABLE `survey` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `survey_key`
--

DROP TABLE IF EXISTS `survey_key`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `survey_key` (
  `Key_ID` int(11) NOT NULL,
  `Survey_ID` int(11) NOT NULL,
  PRIMARY KEY (`Key_ID`),
  KEY `Survey_ID` (`Survey_ID`),
  CONSTRAINT `survey_key_ibfk_1` FOREIGN KEY (`Key_ID`) REFERENCES `key_a` (`Respondent_ID`),
  CONSTRAINT `survey_key_ibfk_2` FOREIGN KEY (`Survey_ID`) REFERENCES `survey` (`Survey_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `survey_key`
--

LOCK TABLES `survey_key` WRITE;
/*!40000 ALTER TABLE `survey_key` DISABLE KEYS */;
/*!40000 ALTER TABLE `survey_key` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-12-14 17:18:04
