import axios from 'axios'

const API_URL = 'http://localhost:4174/api/tickets';

export const getTickets = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error('Error fetching tickets', error);
    throw error;
  }
};

export const createTicket = async (ticket) => {
  try {
    const response = await axios.post(API_URL, { ticket });
    return response.data;
  } catch (error) {
    console.error('Error creating ticket', error);
    throw error;
  }
};

export const updateTicket = async (ticketId, ticket) => {
  try {
    const response = await axios.put(`${API_URL}/${ticketId}`, { ticket });
    return response.data;
  } catch (error) {
    console.error('Error updating ticket', error);
    throw error;
  }
};
