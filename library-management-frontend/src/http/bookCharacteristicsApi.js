import { $host } from "./index";

export const getAllBookCharacteristics = async () => {
  try {
    const { data } = await $host.get("api/bookcharacteristics");
    return data;
  } catch (error) {
    console.error("Error fetching all book characteristics:", error);
    throw error;
  }
};

export const getBookCharacteristicById = async (id) => {
  try {
    const { data } = await $host.get(`api/bookcharacteristics/${id}`);
    return data;
  } catch (error) {
    console.error(`Error fetching book characteristic with ID ${id}:`, error);
    throw error;
  }
};

export const getBooksByAuthorId = async (authorId) => {
  try {
    const { data } = await $host.get(`api/bookcharacteristics/author/${authorId}`);
    return data;
  } catch (error) {
    console.error(`Error fetching books by author ID ${authorId}:`, error);
    throw error;
  }
};

export const addBookCharacteristic = async (bookData) => {
  try {
    const { data } = await $host.post("api/bookcharacteristics", bookData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return data;
  } catch (error) {
    console.error("Error adding book characteristic:", error);
    throw error;
  }
};


export const updateBookCharacteristic = async (id, updatedData) => {
  try {
    await $host.put(`api/bookcharacteristics/${id}`, updatedData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return { message: "Book characteristic updated successfully." };
  } catch (error) {
    console.error(`Error updating book characteristic with ID ${id}:`, error);
    throw error;
  }
};


export const deleteBookCharacteristic = async (id) => {
  try {
    await $host.delete(`api/bookcharacteristics/${id}`);
    return { message: "Book characteristic deleted successfully." };
  } catch (error) {
    console.error(`Error deleting book characteristic with ID ${id}:`, error);
    throw error;
  }
};
