import React, { useState, useEffect } from 'react'

function TicketForm({ ticket = {}, onCancel, onSave }) {
  const [title, setTitle] = useState(ticket.title || '')
  const [description, setDescription] = useState(ticket.description || '')
  const [status, setStatus] = useState(ticket.status || 'Open')

  useEffect(() => {
    if (ticket) {
      setTitle(ticket.title)
      setDescription(ticket.description)
      setStatus(ticket.status)
    }
  }, [ticket])

  const handleSubmit = (e) => {
    e.preventDefault()
    const ticketData = { title, description, status }
    onSave(ticketData) // Pass ticket data to parent component (TicketList)
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>{ticket.id ? 'Edit Ticket' : 'Create Ticket'}</h2>
      <div>
        <label>Title:</label>
        <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} required />
      </div>
      <div>
        <label>Description:</label>
        <textarea value={description} onChange={(e) => setDescription(e.target.value)} required />
      </div>
      <div>
        <label>Status:</label>
        <select value={status} onChange={(e) => setStatus(e.target.value)} required>
          <option value="Open">Open</option>
          <option value="In Progress">In Progress</option>
          <option value="Closed">Closed</option>
        </select>
      </div>
      <div>
        <button type="submit">Save</button>
        <button type="button" onClick={onCancel}>
          Cancel
        </button>
      </div>
    </form>
  )
}

export default TicketForm
