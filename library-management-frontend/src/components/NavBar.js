import React, { useContext } from 'react';
import { Container, Nav, Navbar, Button } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import { Context } from '..';
import {
    MAIN_ROUTE,
    LOGIN_ROUTE,
    REGISTRATION_ROUTE,
    BOOK_LIST_ROUTE,
    USER_BOOKS_ROUTE,
    ADD_BOOK_ROUTE
} from "../utils/consts";

const NavBar = () => {
    const { user } = useContext(Context);
    const navigate = useNavigate();

    console.log("User from context:", user)
    console.log("User Role from NavBar:", user.role);

    const logout = () => {
        user.logout();
        navigate(MAIN_ROUTE);
    };

    const token = localStorage.getItem('token');
    if (token) {
        try {
            const decoded = jwtDecode(token);
            console.log('Decoded token:', decoded);
        } catch (error) {
            console.error('Ошибка при декодировании токена:', error);
        }
    } else {
        console.log('Токен отсутствует в localStorage');
    }

    return (
        <Navbar bg="dark" expand="lg" variant="dark" className="mb-4">
            <Container>
                <Navbar.Brand>
                    <NavLink
                        style={{ fontSize: "24px", color: "white", textDecoration: "none" }}
                        to={MAIN_ROUTE}
                    >
                        Library
                    </NavLink>
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="ml-auto">
                        {user.isAuth ? (
                            <>
                                {user.userInfo.role === "Admin" && (
                                    <Button
                                        className="me-2"
                                        style={{ fontSize: "18px", color: "white" }}
                                        onClick={() => navigate(ADD_BOOK_ROUTE)}
                                        variant="outline-primary"
                                    >
                                        Добавить книгу
                                    </Button>
                                )}

                                <Button
                                    className="me-2"
                                    style={{ fontSize: "18px", color: "white" }}
                                    onClick={() => navigate(USER_BOOKS_ROUTE)}
                                    variant="outline-primary"
                                >
                                    Мои книги
                                </Button>

                                <Button
                                    className="me-2"
                                    style={{ fontSize: "18px", color: "white" }}
                                    onClick={() => navigate(BOOK_LIST_ROUTE)}
                                    variant="outline-primary"
                                >
                                    Список книг
                                </Button>

                                <Button
                                    className="me-2"
                                    style={{ fontSize: "18px", color: "white" }}
                                    onClick={logout}
                                    variant="outline-danger"
                                >
                                    Выйти
                                </Button>
                            </>
                        ) : (
                            <>
                                <Button
                                    className="me-2"
                                    style={{ fontSize: "18px", color: "white" }}
                                    onClick={() => navigate(BOOK_LIST_ROUTE)}
                                    variant="outline-primary"
                                >
                                    Список книг
                                </Button>
                                <Button
                                    className="me-2"
                                    style={{ fontSize: "18px", color: "white" }}
                                    onClick={() => navigate(LOGIN_ROUTE)}
                                    variant="outline-primary"
                                >
                                    Войти
                                </Button>
                                <Button
                                    className="me-2"
                                    style={{ fontSize: "18px", color: "white" }}
                                    onClick={() => navigate(REGISTRATION_ROUTE)}
                                    variant="outline-primary"
                                >
                                    Регистрация
                                </Button>
                            </>
                        )}
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default observer(NavBar);
