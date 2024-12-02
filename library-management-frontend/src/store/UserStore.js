import { makeAutoObservable } from 'mobx';
import { jwtDecode } from 'jwt-decode';

export default class UserStore {
  user = {};
  isAuth = false;

  constructor() {
    makeAutoObservable(this);
    const token = localStorage.getItem('token');
    if (token) {
      this.setToken(token);
    }
  }

  setToken(token) {
    try {
      const decoded = jwtDecode(token);
      this.user = {
        id: decoded.UserId,
        name: decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || 'Unknown',
        role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'User',
      };
      this.isAuth = true;
      localStorage.setItem('isAuth', true);
    } catch (error) {
      console.error('Ошибка при декодировании токена:', error);
      this.isAuth = false;
      this.user = {};
      localStorage.setItem('isAuth', false);
    }
  }


  login(token) {
    this.setToken(token);
    localStorage.setItem('token', token);
  }

  logout() {
    this.user = {};
    this.isAuth = false;
    localStorage.removeItem('token');
    localStorage.clear();
  }

  get isAuthenticated() {
    return this.isAuth;
  }

  get userInfo() {
    return this.user;
  }

  get role() {
    return this.user.role;
  }

  get id() {
    return this.user.id;
  }

}
