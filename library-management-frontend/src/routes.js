import BookList from "./pages/BookListPage"; 
import BookDetails from "./pages/BookDetailsPage"; 
import Management from "./pages/ManagementPage"; 
import UserBooks from "./pages/UserBooksPage"; 
import Auth from "./pages/Auth"; 
import Main from "./pages/MainPage";
import {
  MAIN_ROUTE,
  LOGIN_ROUTE,
  REGISTRATION_ROUTE,
  BOOK_LIST_ROUTE,
  BOOK_DETAILS_ROUTE,
  ADD_BOOK_ROUTE,
  USER_BOOKS_ROUTE
} from "./utils/consts";

export const authRoutes = [
  {
    path: ADD_BOOK_ROUTE, Component: Management,
  },
  {
    path: USER_BOOKS_ROUTE, Component: UserBooks,
  },
];

export const publicRoutes = [
  {
    path: MAIN_ROUTE, Component: Main,
  },
  {
    path: BOOK_LIST_ROUTE, Component: BookList,
  },
  {
    path: BOOK_DETAILS_ROUTE, Component: BookDetails,
  },
  {
    path: LOGIN_ROUTE, Component: Auth,
  },
  {
    path: REGISTRATION_ROUTE, Component: Auth,
  },
];
