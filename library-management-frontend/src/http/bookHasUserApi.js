import { $host, $authHost } from "./index";

export const reserveBook = async (bookCharacteristicsId, userId) => {
  try {
    console.log("Reserving book with characteristics ID:", bookCharacteristicsId, "for user ID:", userId);
    const { data } = await $authHost.post("api/bookhasuser/reserve", {
      bookCharacteristicsId,
      userId,
    });

    console.log("Book reserved successfully:", data);
    return data;
  } catch (error) {
    console.error("Error reserving book:", error);
    throw error;
  }
};

export const returnBook = async (bookId, userId, timeBorrowed) => {
  try {
    console.log("Returning book with ID:", bookId, "for user ID:", userId, "borrowed at:", timeBorrowed);
    const { data } = await $authHost.post("api/bookhasuser/return", {
      bookId,
      userId,
      timeBorrowed,
    });

    console.log("Book returned successfully:", data);
    return data;
  } catch (error) {
    console.error("Error returning book:", error);
    throw error;
  }
};

export const getUserBooks = async (userId) => {
  try {
    const { data } = await $authHost.get(`api/bookhasuser/user/${userId}`);
    return data;
  } catch (error) {
    console.error(`Error fetching books for user ID ${userId}:`, error);
    throw error;
  }
};