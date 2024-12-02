--Admin: root root
--User: qwerty string

-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Дек 02 2024 г., 19:32
-- Версия сервера: 10.1.48-MariaDB
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `LibraryManagement`
--

-- --------------------------------------------------------

--
-- Структура таблицы `Authors`
--

CREATE TABLE `Authors` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Surename` longtext NOT NULL,
  `Birthdate` datetime(6) DEFAULT NULL,
  `Country` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `Authors`
--

INSERT INTO `Authors` (`Id`, `Name`, `Surename`, `Birthdate`, `Country`) VALUES
(1, 'string', 'string', '2024-11-26 07:56:52.938000', 'string'),
(2, 'Ilya', 'Barabolya', '2004-11-28 19:53:37.798000', 'Belarus'),
(3, 'Ilya', 'Barabolya', '2004-11-28 19:53:37.798000', 'Belarus'),
(5, 'q', 'w', '2024-12-02 16:22:59.288000', 'e');

-- --------------------------------------------------------

--
-- Структура таблицы `BookCharacteristics`
--

CREATE TABLE `BookCharacteristics` (
  `Id` int(11) NOT NULL,
  `ISBN` int(11) NOT NULL,
  `Title` longtext NOT NULL,
  `Genre` longtext NOT NULL,
  `Description` longtext NOT NULL,
  `AuthorId` int(11) NOT NULL,
  `ImgPath` longtext NOT NULL,
  `CheckoutPeriod` int(11) NOT NULL,
  `BookCount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `BookCharacteristics`
--

INSERT INTO `BookCharacteristics` (`Id`, `ISBN`, `Title`, `Genre`, `Description`, `AuthorId`, `ImgPath`, `CheckoutPeriod`, `BookCount`) VALUES
(5, 41838313, 'Процесс', 'Философский роман', 'философский роман Франца Кафки, посмертно опубликованный в 1925 году. «Процесс» входит во «Всемирную библиотеку», а также в список 100 книг века по версии Le Monde.', 2, '/images/64871d95-c7bb-42a7-a754-a9d2ee47196c_150327.jpg', 30, 6),
(6, 412972315, 'Война и мир', 'Роман', 'роман-эпопея Льва Николаевича Толстого, описывающий русское общество в эпоху войн против Наполеона в 1805—1812 годах. Эпилог романа доводит повествование до 1820 года.', 3, '/images/d11d6e1e-466d-41fa-a4e0-c46c549fdcff_w-p.jpg', 30, 10),
(7, 68431351, 'Цветы для Элджернона', 'Научная фантастика', 'научно-фантастический рассказ Дэниела Киза. Первоначально издан в апрельском номере «Журнала фэнтези и научной фантастики» за 1959 год. Премия «Хьюго» за лучший короткий научно-фантастический рассказ.', 2, '/images/ad09cab6-c9c7-4f9c-8cdf-d95722f8db11_i495423.jpg', 30, 9),
(9, 6354512, 'Солярис', 'Философский роман', 'величайшее из произведений Станислава Лема, ставшее классикой не только фантастики, но и всей мировой прозы XX века. ', 3, '/images/67df0f55-f3d0-426e-a362-f8f4181e4aab_796fc29e2b3f014ca1cf0.jpg', 30, 11),
(10, 8641389, 'О дивный новый мир', 'Философский роман', 'антиутопия Олдоса Хаксли «О, дивный новый мир» — наивысшая точка в развитии его сатирического гения и одновременно начало утраты веры в человечество.', 2, '/images/b2f634f1-28d7-4173-a162-11180532b7f6_9a7061f6f34da669e5eac.jpg', 30, 10),
(12, 7894652, 'Мечтают ли андроиды об электроовцах?', 'Научная фантастика', '«Мечта́ют ли андроиды об электроо́вцах?» или «Снятся ли андроидам электроовцы?» — научно-фантастический роман американского писателя Филипа Дика, предшественник киберпанка, написанный в 1968 году.', 2, '/images/2f9e2848-12f7-4756-81af-21dca15ca529_22601292.jpg', 20, 5);

-- --------------------------------------------------------

--
-- Структура таблицы `BookHasUsers`
--

CREATE TABLE `BookHasUsers` (
  `BookId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `TimeBorrowed` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `BookHasUsers`
--

INSERT INTO `BookHasUsers` (`BookId`, `UserId`, `TimeBorrowed`) VALUES
(2, 8, '2024-12-01 23:51:27.768663'),
(21, 6, '2024-12-01 18:50:51.517535');

-- --------------------------------------------------------

--
-- Структура таблицы `Books`
--

CREATE TABLE `Books` (
  `Id` int(11) NOT NULL,
  `BookCharacteristicsId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `Books`
--

INSERT INTO `Books` (`Id`, `BookCharacteristicsId`) VALUES
(1, 5),
(2, 5),
(3, 5),
(4, 5),
(5, 5),
(6, 5),
(7, 5),
(8, 5),
(9, 5),
(10, 5),
(11, 6),
(12, 6),
(13, 6),
(14, 6),
(15, 6),
(16, 6),
(17, 6),
(18, 6),
(19, 6),
(20, 6),
(21, 7),
(22, 7),
(23, 7),
(24, 7),
(25, 7),
(26, 7),
(27, 7),
(28, 7),
(29, 7),
(30, 7),
(41, 9),
(42, 9),
(43, 9),
(44, 9),
(45, 9),
(46, 9),
(47, 9),
(48, 9),
(49, 9),
(50, 9),
(51, 10),
(52, 10),
(53, 10),
(54, 10),
(55, 10),
(56, 10),
(57, 10),
(58, 10),
(59, 10),
(60, 10),
(71, 12),
(72, 12),
(73, 12),
(74, 12),
(75, 12);

-- --------------------------------------------------------

--
-- Структура таблицы `Users`
--

CREATE TABLE `Users` (
  `Id` int(11) NOT NULL,
  `Email` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `Role` longtext NOT NULL,
  `Name` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `Users`
--

INSERT INTO `Users` (`Id`, `Email`, `Password`, `Role`, `Name`) VALUES
(1, 'string', 'RzKH+CmNunFjqJeQiVj3wOrnM+JdLgJ5kuou3JvtL6g=', 'User', 'string'),
(2, 'somemail', '$2a$11$8dOPmejtuYiL0ZpkbVU2Z.nYFcCWkUqlZ7R807q9680IYsMU0UVI2', 'User', 'string'),
(3, 'qweert', '$2a$11$4U8Kp4I3Rc2Jgb71AOaWxONMkQU1nBmQGtXT/HfDHaYMLIogWwv3.', 'User', 'string'),
(4, 'qweqew', '$2a$11$JHxtvtSdd/uGyH4XvcnDVu1Y6Ntogvkbu8YZYqq8zqTQ8ccNHQQty', 'User', 'string'),
(5, 'qwe', '$2a$11$qZ2yE1fSay39sot0Tn4KEOGboozSJbuE3mN6EHNby/9mDUs9bE7Xe', 'User', 'zxc'),
(6, 'qwerty', '$2a$11$5wQ6l2FBSSfI42zyg5tw7OZW1zx4hkABnrw0bnuO7HIcuoATViIga', 'User', 'string'),
(7, 'somemail@test.com', '$2a$11$yq9trIlhMWJiZes8LYoAIOTig8WtT0u9jWnTknfpoopUDUEDm5Vri', 'User', 'Dmitry'),
(8, 'root', '$2a$11$uVOzyWOzzK.u79JZfbTF6O9yb4NetyS01tqgigVyQknYRxb9ogbxW', 'Admin', 'root');

-- --------------------------------------------------------

--
-- Структура таблицы `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20241125233225_InitialCreate', '8.0.11');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Authors`
--
ALTER TABLE `Authors`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `BookCharacteristics`
--
ALTER TABLE `BookCharacteristics`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_BookCharacteristics_AuthorId` (`AuthorId`);

--
-- Индексы таблицы `BookHasUsers`
--
ALTER TABLE `BookHasUsers`
  ADD PRIMARY KEY (`BookId`,`UserId`),
  ADD KEY `IX_BookHasUsers_UserId` (`UserId`);

--
-- Индексы таблицы `Books`
--
ALTER TABLE `Books`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Books_BookCharacteristicsId` (`BookCharacteristicsId`);

--
-- Индексы таблицы `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Authors`
--
ALTER TABLE `Authors`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `BookCharacteristics`
--
ALTER TABLE `BookCharacteristics`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT для таблицы `Books`
--
ALTER TABLE `Books`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=76;

--
-- AUTO_INCREMENT для таблицы `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `BookCharacteristics`
--
ALTER TABLE `BookCharacteristics`
  ADD CONSTRAINT `FK_BookCharacteristics_Authors_AuthorId` FOREIGN KEY (`AuthorId`) REFERENCES `Authors` (`Id`) ON DELETE CASCADE;

--
-- Ограничения внешнего ключа таблицы `BookHasUsers`
--
ALTER TABLE `BookHasUsers`
  ADD CONSTRAINT `FK_BookHasUsers_Books_BookId` FOREIGN KEY (`BookId`) REFERENCES `Books` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_BookHasUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE;

--
-- Ограничения внешнего ключа таблицы `Books`
--
ALTER TABLE `Books`
  ADD CONSTRAINT `FK_Books_BookCharacteristics_BookCharacteristicsId` FOREIGN KEY (`BookCharacteristicsId`) REFERENCES `BookCharacteristics` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
