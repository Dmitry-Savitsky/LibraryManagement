import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useContext } from 'react';
import { Row, Col, Form, Button } from 'react-bootstrap';
import { NavLink, useLocation, useNavigate } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { Context } from '../index';
import { login, register } from '../http/userApi';
import { LOGIN_ROUTE, REGISTRATION_ROUTE, MAIN_ROUTE } from '../utils/consts';

const Auth = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { user } = useContext(Context);

  const isLogin = location.pathname === LOGIN_ROUTE;

  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = async () => {
    try {
      let token;
      if (isLogin) {
        token = await login(email, password);
      } else {
        token = await register(name, email, password);
      }

      console.log("Токен передается в UserStore:", token); user.login(token); toast.success("Успешно!", { position: "top-center", autoClose: 2000 });

      navigate(MAIN_ROUTE);
    } catch (error) {
      console.error("Ошибка:", error);
      toast.error(`Ошибка: ${error.response?.data?.message || "Что-то пошло не так"}`, {
        position: "top-center",
        autoClose: 2000,
      });
    }
  };






  return (
    <>
      <Row style={{ width: '100%', height: '80vh', overflowX: 'hidden' }}>
        <Col className="d-flex flex-column justify-content-center align-items-center">
          <h3 style={{ color: 'GrayText' }}>{isLogin ? 'АВТОРИЗАЦИЯ' : 'РЕГИСТРАЦИЯ'}</h3>
          <Form className="col-5">
            <Form.Group className="mb-3">
              {!isLogin && (
                <Form.Control
                  className="mb-2"
                  type="text"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  placeholder="Имя"
                />
              )}
              <Form.Control
                className="mb-2"
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
              />
              <Form.Control
                className="mb-2"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Пароль"
              />
              <Button onClick={handleSubmit} variant="dark" className="w-100">
                {isLogin ? 'Войти' : 'Зарегистрироваться'}
              </Button>
            </Form.Group>
            {isLogin ? (
              <div className="text-center">
                <span>Нет аккаунта? </span>
                <NavLink to={REGISTRATION_ROUTE} className="text-decoration-none" style={{ color: 'brown' }}>
                  Зарегистрироваться
                </NavLink>
              </div>
            ) : (
              <div className="text-center">
                <span>Уже есть аккаунт? </span>
                <NavLink to={LOGIN_ROUTE} className="text-decoration-none" style={{ color: 'brown' }}>
                  Войти
                </NavLink>
              </div>
            )}
          </Form>
        </Col>
      </Row>
      <ToastContainer />
    </>
  );
};

export default observer(Auth);
