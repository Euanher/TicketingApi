import React, { useEffect, useState } from 'react'
import TicketForm from './TicketForm'
import { getTickets, createTicket, updateTicket } from '../api' // Import the API methods

const TicketList = () => {
  const [tickets, setTickets] = useState([])
  const [editingTicket, setEditingTicket] = useState(null)

  // Fetch tickets when component mounts
  useEffect(() => {
    const fetchTickets = async () => {
      try {
        const fetchedTickets = await getTickets()
        setTickets(fetchedTickets)
      } catch (error) {
        console.error('Error fetching tickets:', error)
      }
    }
    fetchTickets()
  }, [])

  // Handle editing ticket
  const handleEdit = (ticket) => {
    setEditingTicket(ticket)
  }

  // Handle canceling editing
  const handleCancel = () => {
    setEditingTicket(null)
  }

  // Handle creating or updating a ticket
  const handleTicketSave = async (ticketData) => {
    try {
      if (editingTicket) {
        // Update existing ticket
        await updateTicket(editingTicket.id, ticketData)
      } else {
        // Create new ticket
        await createTicket(ticketData)
      }
      // Refresh tickets list after save
      const fetchedTickets = await getTickets()
      setTickets(fetchedTickets)
      setEditingTicket(null)
    } catch (error) {
      console.error('Error saving ticket:', error)
    }
  }

  return (
    <div>
      <h1>Tickets</h1>
      {editingTicket ? (
        <TicketForm ticket={editingTicket} onCancel={handleCancel} onSave={handleTicketSave} />
      ) : (
        <div>
          <ul>
            {tickets.map((ticket) => (
              <li key={ticket.id}>
                {ticket.title} - {ticket.status}
                <button onClick={() => handleEdit(ticket)}>Edit</button>
              </li>
            ))}
          </ul>
          <TicketForm onSave={handleTicketSave} />
        </div>
      )}
    </div>
  )
}

export default TicketList
